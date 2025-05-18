using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Helpers;
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
        private readonly IBaseRepository<ShopModel> _shopRepository;
        private readonly IBaseRepository<CategoriesModel> _categoriesRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IScoreProductService _scoreProductService;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<ProductModel> baseRepository, IProductRepository productRepository, IBaseRepository<ShopModel> shopRepository,
            IBaseRepository<CategoriesModel> categoriesRepository, IImageRepository imageRepository, IDiscountRepository discountRepository,
            IScoreProductService scoreProductService, IStockRepository stockRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _categoriesRepository = categoriesRepository;
            _imageRepository = imageRepository;
            _discountRepository = discountRepository;
            _scoreProductService = scoreProductService;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<bool> ActiveProduct(int idProduct)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(idProduct);
                if (result == null) throw new FailureRequestException(404, "Id do produto não localizado");
                if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Produto ja esta ativo");
                result.status = StandartStatus.Ativo.ToString();
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

        public async Task<ProductDto> CreateProduct(ProductCreateDto productModel)
        {
            try
            {
                var findShop = await _shopRepository.SelectByIdAsync( productModel.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategory = await _categoriesRepository.SelectByIdAsync(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                var result = await _productRepository.GetProductByName(productModel.name, productModel.shopId);
                if(result != null) throw new FailureRequestException(409, "Produto ja cadastrado.");
                ProductModel product = _mapper.Map<ProductModel>(productModel);
                product.status = StandartStatus.Ativo.ToString();
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

        public async Task<bool> DisableProduct(int idProduct)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(idProduct);
                if (result == null) throw new FailureRequestException(404, "Id do produto não localizado");
                if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Produto ja esta ativo");
                result.status = StandartStatus.Desabilitado.ToString();
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

        public async Task<List<ProductDto>> GetAllProductByCategoryId(int idCategory, int idShop)
        {
            try
            {
                var findShop = await _shopRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategory = await _categoriesRepository.SelectByIdAsync(idCategory);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                var findProduct = await _productRepository.GetAllProductByCategoryId(idCategory, idShop);
                if (findProduct == null) return new List<ProductDto>();
                return _mapper.Map<List<ProductDto>>(findProduct);
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

        public async Task<List<ProductDto>> GetAllProductsByShopId(int idShop,FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                var findShop = await _shopRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findProduct = await _productRepository.GetAllProductsByShopId(status, idShop);
                if (findProduct == null) return new List<ProductDto>();
                return _mapper.Map<List<ProductDto>>(findProduct);
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

       
        public async Task<ProductDto> GetProductById(int id)
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

        public async Task<ProductDto> GetProductByName(string name, int idShop)
        {
            try
            {
                var findShop = await _shopRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findProduct = await _productRepository.GetProductByName(name, idShop);
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
       
        public async Task<bool> UpdateProduct(ProductUpdateDto productModel)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(productModel.idProduct);
                if (result == null) throw new FailureRequestException(409, "id do Produto nao localizado");
                var findCategory = await _categoriesRepository.SelectByIdAsync(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                var findNameProdutc = await _productRepository.GetProductByName(productModel.name, result.shopId);
                if (findNameProdutc != null) throw new FailureRequestException(409, "Produto com esse nome ja cadastrado");
              
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

        public async Task<List<ProductFullDto>> GetAllProductsFullActive()
        {
            try
            {
                var products = await _productRepository.GetAllProductsOnStock();
                if (products == null || !products.Any()) throw new FailureRequestException(409, "Nenhum produto encontrado para essa loja.");

                var productList = new List<ProductFullDto>();

                foreach (var product in products)
                {
                    var findCategory = await _categoriesRepository.SelectByIdAsync(product.categoriesId);
                    if (findCategory == null) throw new FailureRequestException(409, "id da categoria nao localizada.");

                    var findImage = await _imageRepository.GetImagesByIdProduct(product.id);
                    var findScore = await _scoreProductService.GetScoreProductByProductId(product.id);
                    var findDiscount = await _discountRepository.GetDiscountByProductId(product.id);
                    var findShop = await _shopRepository.SelectByIdAsync(product.shopId);
                    var stockProduct = await _stockRepository.GetStockByProductId(product.id);
                    var listUrls = findImage.Select(img => img.url).ToList();
                    var model = new ProductFullDto
                    {
                        Id = product.id,
                        ShopId = product.shopId,
                        NameShop = findShop.name,
                        Name = product.name,
                        Price = product.price,
                        Categoria = findCategory.name,
                        ImageUrl = listUrls,
                        Description = product.description,
                        IsNew = (DateTime.UtcNow - product.createdAt).TotalDays <= 30,
                        Stock = stockProduct == null ? null : (stockProduct.amount <= 0 ? null : _mapper.Map<StockDto>(stockProduct)),
                        Rating = findScore != null ? findScore.First().Value : 0,
                        Discount = findDiscount != null ? new PercentDiscountDto { Value = findDiscount.percentDiscount } : null
                    };
                    
                    productList.Add(model);
                }

                return productList;
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

        public async Task<ProductFullDto> GetProductFullById(int id)
        {
            try
            {
                var findProduct = await _baseRepository.SelectByIdAsync(id);
                if (findProduct == null) throw new FailureRequestException(409, "id do Produto nao localizado.");
                var findCategory = await _categoriesRepository.SelectByIdAsync(findProduct.categoriesId);
                if (findCategory == null) throw new FailureRequestException(409, "id da categoria nao localizada.");
                var findImage = await _imageRepository.GetImagesByIdProduct(findProduct.id);
                var findScore = await _scoreProductService.GetScoreProductByProductId(findProduct.id);
                var findDiscount = await _discountRepository.GetDiscountByProductId(findProduct.id);
                var stockProduct = await _stockRepository.GetStockByProductId(findProduct.id);
                var findShop = await _shopRepository.SelectByIdAsync(findProduct.shopId);
                var listUrls = new List<string>();
                foreach (ImageModel image in findImage)
                {
                    listUrls.Add(image.url);
                }
                var model = new ProductFullDto
                {
                    Id = findProduct.id,
                    ShopId = findProduct.shopId,
                    NameShop = findShop.name,
                    Name = findProduct.name,
                    Price = findProduct.price,
                    Categoria = findCategory.name,
                    ImageUrl = listUrls, // Pegando a primeira imagem
                    Description = findProduct.description,
                    Stock = stockProduct == null ? new StockDto() : (stockProduct.amount <= 0 ? new StockDto() : _mapper.Map<StockDto>(stockProduct)),
                    IsNew = (DateTime.UtcNow - findProduct.createdAt).TotalDays <= 30, 
                    Rating = findScore != null ? findScore.First().Value : 0, // Média das avaliações
                    Discount = findDiscount !=null ? new PercentDiscountDto { Value = findDiscount.percentDiscount } : new PercentDiscountDto()
                };
                return model;
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

        public async Task<ProductFullDto> GetProductFullByName(string name, int idShop)
        {
            try
            {
                var findProduct = await _productRepository.GetProductByName(name, idShop);
                if (findProduct == null) throw new FailureRequestException(409, "id do Produto nao localizado.");
                var findCategory = await _categoriesRepository.SelectByIdAsync(findProduct.categoriesId);
                if (findCategory == null) throw new FailureRequestException(409, "id da categoria nao localizada.");
                var findImage = await _imageRepository.GetImagesByIdProduct(findProduct.id);
                var findScore = await _scoreProductService.GetScoreProductByProductId(findProduct.id);
                var findDiscount = await _discountRepository.GetDiscountByProductId(findProduct.id);
                var stockProduct = await _stockRepository.GetStockByProductId(findProduct.id);
                var findShop = await _shopRepository.SelectByIdAsync(findProduct.shopId);
                

                var listUrls = new List<string>();
                foreach (ImageModel image in findImage)
                {
                    listUrls.Add(image.url);
                }
                var model = new ProductFullDto
                {
                    Id = findProduct.id,
                    ShopId = findProduct.shopId,
                    NameShop = findShop.name,
                    Name = findProduct.name,
                    Price = findProduct.price,
                    Categoria = findCategory.name,
                    ImageUrl = listUrls, // Pegando a primeira imagem
                    Description = findProduct.description,
                    IsNew = (DateTime.UtcNow - findProduct.createdAt).TotalDays <= 30,
                    Stock = stockProduct == null ? null : (stockProduct.amount <= 0 ? null : _mapper.Map<StockDto>(stockProduct)),
                    Rating = findScore != null ? findScore.First().Value : 0, // Média das avaliações
                    Discount = findDiscount != null ? new PercentDiscountDto { Value = findDiscount.percentDiscount } : null
                };
                return model;
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
