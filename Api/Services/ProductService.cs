using ApiEstoque.Constants;
using ApiEstoque.Dto.Image;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using SharedBase.Repository;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ApiEstoque.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<ProductModel> _baseRepository;
        private readonly IProductRepository _productRepository;

        private readonly IShopService _shopService;
        private readonly ICategoriesService _categoriesService;
        private readonly IImageService _imageService;

        private readonly IDiscountService _discountService;
        
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<ProductModel> baseRepository, 
            IProductRepository productRepository, IShopService shopService,
            ICategoriesService categoriesService, IImageService imageService,
            IDiscountService discountService,
            IStockService stockService,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
            _shopService = shopService;
            _categoriesService = categoriesService;
            _imageService = imageService;
            _discountService = discountService;
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

                ProductModel product = await _baseRepository.InsertAsync(_mapper.Map<ProductModel>(productModel));
                if (product == null) throw new FailureRequestException(409, "Falha ao cadastrar produto.");

                var modelImage = new ImageCreateDto()
                {
                    images = productModel.images,
                    shopId = productModel.shopId,
                    productId = product.Id
                };
                var image = await _imageService.Create(modelImage);
                if (image == null) throw new FailureRequestException(409, "Falha ao cadastrar imagem.");
                return _mapper.Map<ProductDto>(product);

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

                if (productModel.removedUrls != null && productModel.removedUrls.Any())
                {
                    foreach (var url in productModel.removedUrls)
                    {
                        await _imageService.DeleteByUrl(url); // Remove do banco e do bucket
                    }
                }
                if (productModel.NewImages != null && productModel.NewImages.Any())
                {
                    var imageDto = new ImageCreateDto
                    {
                        images = productModel.NewImages,
                        productId = result.Id,
                        shopId = result.shopId
                    };
                    await _imageService.Create(imageDto); // Faz upload no bucket e salva URLs no banco
                }

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

        public async Task<ProductViewModel> GetAllWithDetailsByIdShop(Guid idShop, FilterGetRoutes status = FilterGetRoutes.All, int limit = 20, int page = 0, string category = "")
        {
            try
            {
                var shop = await _shopService.GetById(idShop);
                if (shop == null) throw new FailureRequestException(404, "Nenhum shop localizado.");

                var products = await _productRepository.GetAllByIdShop(idShop, status, limit, page, category: category);
                if (products == null || !products.Any()) return new ProductViewModel();

                // Coleta IDs em lote
                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var productIds = products.Select(p => p.Id).ToList();

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
                    Id = product.Id,
                    ShopId = product.shopId,
                    NameShop = shop.name,
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var catName) ? catName : "Não Categorizado",
                    UrlImages = imagesDict.TryGetValue(product.Id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.CreatedAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.Id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.Id, out var discount)
                        ? new PercentDiscountDto
                        {
                            id = discount.id,
                            updatedAt = discount.updatedAt,
                            percentDiscount = discount.percentDiscount
                        }
                        : null
                }).ToList();

                var countProdutos = await _productRepository.CountProducts(category: category);
                return new ProductViewModel()
                {
                    Products = result,
                    SizeList = countProdutos
                };
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


        //Controller sem authenticação
        public async Task<ProductViewModel> GetAllWithDetails(int limit = 20, int page = 0, string category = "")
        {
            try
            {
                var products = await _productRepository.SelectAllByStatusAsync(limit:limit,page:page, category: category);
                if (products == null || !products.Any())
                    return new ProductViewModel();

                // Coleta IDs para busca em lote
                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var shopIds = products.Select(p => p.shopId).Distinct().ToList();
                var productIds = products.Select(p => p.Id).ToList();

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
                    Id = product.Id,
                    ShopId = product.shopId,
                    NameShop = shopDict.TryGetValue(product.shopId, out var shopName) ? shopName : "Loja Desconhecida",
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var categoryName) ? categoryName : "Não Categorizado",
                    UrlImages = imageDict.TryGetValue(product.Id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.CreatedAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.Id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.Id, out var discount)
                        ? new PercentDiscountDto {
                            id = discount.id,
                            updatedAt = discount.updatedAt,
                            percentDiscount = discount.percentDiscount
                        }
                        : null
                }).ToList();

                var countProdutos = await _productRepository.CountProducts(category: category);
                return new ProductViewModel()
                {
                    Products = result,
                    SizeList = countProdutos
                };
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

        public async Task<ProductViewModel> GetAllWithDetailsLikeName(string name)
        {
            try
            {
                var products = await _productRepository.GetAllLikeName(name);

                if (products == null || !products.Any())
                    throw new FailureRequestException(404, "Nenhum produto localizado.");

                var categoryIds = products.Select(p => p.categoriesId).Distinct().ToList();
                var shopIds = products.Select(p => p.shopId).Distinct().ToList();
                var productIds = products.Select(p => p.Id).ToList();

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
                    Id = product.Id,
                    ShopId = product.shopId,
                    NameShop = shopDict.TryGetValue(product.shopId, out var shopName) ? shopName : "Loja Desconhecida",
                    Name = product.name,
                    Price = product.price,
                    Categoria = categoryDict.TryGetValue(product.categoriesId, out var categoryName) ? categoryName : "Não Categorizado",
                    UrlImages = imageDict.TryGetValue(product.Id, out var urls) ? urls : new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.CreatedAt).TotalDays <= 7,
                    Stock = stockDict.TryGetValue(product.Id, out var stock) && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discountDict.TryGetValue(product.Id, out var discount)
                        ? new PercentDiscountDto
                        {
                            id = discount.id,
                            updatedAt = discount.updatedAt,
                            percentDiscount = discount.percentDiscount
                        }
                        : null
                }).ToList();

                var countProdutos = await _productRepository.CountProducts();
                return new ProductViewModel()
                {
                    Products = result,
                    SizeList = countProdutos
                };
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

        //Apenas em Services
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

        public async Task<ProductDetailsDto> GetWithDetailsById(Guid id)
        {
            try
            {
                // Busca o produto específico
                var product = await _baseRepository.SelectByIdAsync(id);
                if (product == null) throw new FailureRequestException(404, "Nenhum produto localizado.");

                // Busca os dados relacionados apenas desse produto
                var category = await _categoriesService.GetById(product.categoriesId);
                var shop = await _shopService.GetById(product.shopId);
                var images = await _imageService.GetAllByProductsIds(new List<Guid> { product.Id });
                var stock = await _stockService.GetByProductId(product.Id);
                var discount = await _discountService.GetByProductId(product.Id);

                // Monta o DTO de retorno
                var dto = new ProductDetailsDto
                {
                    Id = product.Id,
                    ShopId = product.shopId,
                    NameShop = shop?.name ?? "Loja Desconhecida",
                    Name = product.name,
                    Price = product.price,
                    Categoria = category?.name ?? "Não Categorizado",
                    UrlImages = images?.Select(i => i.url).ToList() ?? new List<string>(),
                    Description = product.description,
                    IsNew = (DateTime.UtcNow - product.CreatedAt).TotalDays <= 7,
                    Stock = stock != null && stock.amount > 0
                        ? _mapper.Map<StockDto>(stock)
                        : null,
                    Discount = discount != null && discount.percentDiscount > 0
                        ? new PercentDiscountDto
                        {
                            id = discount.id,
                            updatedAt = discount.updatedAt,
                            percentDiscount = discount.percentDiscount
                        }
                        : null
                };

                return dto;
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
