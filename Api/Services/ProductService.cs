using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System.Net.NetworkInformation;

namespace ApiEstoque.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<ProductModel> _baseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShopService _shopService;
        private readonly ICategoriesService _categoriesService;
        private readonly IScoreProductService _scoreProductService;
        private readonly IImageService _imageService;

        private readonly IDiscountService _discountService;
        
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<ProductModel> baseRepository, 
            IProductRepository productRepository, IShopService shopService,
            ICategoriesService categoriesService, IImageService imageService,
            IDiscountService discountService,
            IScoreProductService scoreProductService, IStockService stockService,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
            _shopService = shopService;
            _categoriesService = categoriesService;
            _imageService = imageService;
            _discountService = discountService;
            _scoreProductService = scoreProductService;
            _stockService = stockService;
            _mapper = mapper;
        }

      
        public async Task<ProductDto> Create(ProductCreateDto productModel)
        {
            try
            {
                var findShop = await _shopService.GetById( productModel.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");

                var findCategory = await _categoriesService.GetById(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");

                var result = await _productRepository.GetByNameAndIdShop(productModel.name, productModel.shopId);
                if(result != null) throw new FailureRequestException(409, "Produto ja cadastrado.");

                ProductModel product = _mapper.Map<ProductModel>(productModel);
                return _mapper.Map<ProductDto>(await _baseRepository.InsertAsync(product));

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Update(ProductUpdateDto productModel)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(productModel.idProduct);
                if (result == null) throw new FailureRequestException(409, "id do Produto nao localizado");

                var findCategory = await _categoriesService.GetById(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");

          
                result.name = productModel.name;
                result.price = productModel.price;
                result.categoriesId = productModel.categoriesId;
                result.description = productModel.description;

                return await _baseRepository.UpdateAsync(result);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ChangeStatus(Guid idProduct, bool isActive)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(idProduct);
                if (result == null) throw new FailureRequestException(404, "Id do produto não localizado");


                if (isActive == true)
                {
                   if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(404, "Produto já esta ativo.");
                    result.status = StandartStatus.Ativo.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }
                else
                {
                    if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(404, "Produto já esta desativado.");
                    result.status = StandartStatus.Desabilitado.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<List<ProductDetailsDto>> GetAllWithDetails()
        {
            try
            {
                var products = await _baseRepository.SelectAllByStatusAsync();
                if (products == null || !products.Any())
                    return new List<ProductDetailsDto>();

                // Coleta IDs para busca em lote
                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var shopIds = products.Select(p => p.shopId).Distinct().ToList();
                var productIds = products.Select(p => p.id).ToList();

                // Buscas em lote
                var categories = await _categoriesService.GetAllByIds(categoryIds);
                var shops = await _shopService.GetAllByIds(shopIds);
                var images = await _imageService.GetAllByProductsIds(productIds);
                var stocks = await _stockService.GetAllByProductsIds(productIds);
                var discounts = await _discountService.GetAllByProductsIds(productIds);

                // Conversões para dicionário para acesso rápido
                var categoryDict = categories.ToDictionary(c => c.id, c => c.name);
                var shopDict = shops.ToDictionary(s => s.id, s => s.name);
                var imageDict = images.GroupBy(i => i.productId).ToDictionary(g => g.Key, g => g.Select(i => i.url).ToList());
                var stockDict = stocks.ToDictionary(s => s.productId);
                var discountDict = discounts.ToDictionary(d => d.productId);

                var result = products.Select(product => new ProductDetailsDto
                {
                    Id = product.id,
                    ShopId = product.shopId,
                    NameShop = shopDict.TryGetValue(product.shopId, out var shopName) ? shopName : "Loja Desconhecida",
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var categoryName) ? categoryName : "Não Categorizado",
                    UrlImages = imageDict.TryGetValue(product.id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.createdAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.id, out var discount)
                        ? new PercentDiscountDto { Value = discount.percentDiscount }
                        : null
                }).ToList();

                return result;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<ProductDetailsDto>> GetAllWithDetailsByIdShop(Guid idShop)
        {
            try
            {
                var shop = await _shopService.GetById(idShop);
                if (shop == null) throw new FailureRequestException(404, "Nenhum shop localizado.");

                var products = await _productRepository.GetAllByIdShop(idShop);
                if (products == null || !products.Any()) throw new FailureRequestException(404, "Nenhum produto localizado.");

                // Coleta IDs em lote
                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var productIds = products.Select(p => p.id).ToList();

                // Busca em lote
                var categories = await _categoriesService.GetAllByIds(categoryIds);
                var images = await _imageService.GetAllByProductsIds(productIds);
                var stocks = await _stockService.GetAllByProductsIds(productIds);
                var discounts = await _discountService.GetAllByProductsIds(productIds);

                // Indexação para lookup rápido
                var categoryDict = categories.ToDictionary(c => c.id, c => c.name);
                var imagesDict = images.GroupBy(i => i.productId).ToDictionary(g => g.Key, g => g.Select(i => i.url).ToList());
                var stockDict = stocks.ToDictionary(s => s.productId);
                var discountDict = discounts.ToDictionary(d => d.productId);

                var result = products.Select(product => new ProductDetailsDto
                {
                    Id = product.id,
                    ShopId = product.shopId,
                    NameShop = shop.name,
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var catName) ? catName : "Não Categorizado",
                    UrlImages = imagesDict.TryGetValue(product.id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.createdAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.id, out var discount)
                        ? new PercentDiscountDto { Value = discount.percentDiscount }
                        : null
                }).ToList();

                return result;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<ProductDetailsDto>> GetAllWithDetailsLikeName(string name)
        {
            try
            {
                var products = await _productRepository.GetAllLikeName(name);

                if (products == null || !products.Any())
                    throw new FailureRequestException(404, "Nenhum produto localizado.");

                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var shopIds = products.Select(p => p.shopId).Distinct().ToList();
                var productIds = products.Select(p => p.id).ToList();

                var shops = await _shopService.GetAllByIds(shopIds);
                var categories = await _categoriesService.GetAllByIds(categoryIds);
                var images = await _imageService.GetAllByProductsIds(productIds);
                var stocks = await _stockService.GetAllByProductsIds(productIds);
                var discounts = await _discountService.GetAllByProductsIds(productIds);

                var categoryDict = categories.ToDictionary(c => c.id, c => c.name);
                var shopDict = shops.ToDictionary(s => s.id, s => s.name);
                var imageDict = images.GroupBy(i => i.productId).ToDictionary(g => g.Key, g => g.Select(i => i.url).ToList());
                var stockDict = stocks.ToDictionary(s => s.productId);
                var discountDict = discounts.ToDictionary(d => d.productId);

                var result = products.Select(product => new ProductDetailsDto
                {
                    Id = product.id,
                    ShopId = product.shopId,
                    NameShop = shopDict.TryGetValue(product.shopId, out var shopName) ? shopName : "Loja Desconhecida",
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var categoryName) ? categoryName : "Não Categorizado",
                    UrlImages = imageDict.TryGetValue(product.id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.createdAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.id, out var discount)
                        ? new PercentDiscountDto { Value = discount.percentDiscount }
                        : null
                }).ToList();

                return result;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            try
            {
                var findProduct = await _baseRepository.SelectByIdAsync(id);
                if (findProduct == null) throw new FailureRequestException(404, "Nenhum produto localizado.");
                return _mapper.Map<ProductDto>(findProduct);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
