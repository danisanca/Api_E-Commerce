using ApiEstoque.Dto.Product;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System.Net.NetworkInformation;

namespace ApiEstoque.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IShopRepository shopRepository, 
            IImageRepository imageRepository, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _imageRepository = imageRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<bool> ActiveProduct(int idProduct)
        {
            try
            {
                var result = await _productRepository.GetProductById(idProduct);
                if (result == null) throw new FailureRequestException(404, "Id do produto não localizado");
                if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Produto ja esta ativo");
                result.status = StandartStatus.Ativo.ToString();
                await _productRepository.UpdateProduct(result);
                return true;
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
                var findShop = await _shopRepository.GetShopById( productModel.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategory = await _categoriesRepository.GetCategoriesById(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                if(findCategory.shopId != productModel.shopId) throw new FailureRequestException(404, "Categoria nao pertence ao shopId");
                var result = await _productRepository.GetProductByName(productModel.name, productModel.shopId);
                if(result != null) throw new FailureRequestException(409, "Produto ja cadastrado.");
                if (productModel.imageId !=null)
                {
                    var findUrlImage = await _imageRepository.GetImageById((int)productModel.imageId);
                    if (findUrlImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada.");
                }
                ProductModel product = _mapper.Map<ProductModel>(productModel);
                product.status = StandartStatus.Ativo.ToString();
                await _productRepository.CreateProduct(product);
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

        public async Task<bool> DisableProduct(int idProduct)
        {
            try
            {
                var result = await _productRepository.GetProductById(idProduct);
                if (result == null) throw new FailureRequestException(404, "Id do produto não localizado");
                if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Produto ja esta ativo");
                result.status = StandartStatus.Desabilitado.ToString();
                await _productRepository.UpdateProduct(result);
                return true;
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
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategory = await _categoriesRepository.GetCategoriesById(idCategory);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                var findProduct = await _productRepository.GetAllProductByCategoryId(idCategory, idShop);
                if (findProduct == null) throw new FailureRequestException(404, "Nenhum produto localizado.");
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
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findProduct = await _productRepository.GetAllProductsByShopId(status, idShop);
                if (findProduct == null) throw new FailureRequestException(404, "Nenhum produto localizado.");
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
                var findProduct = await _productRepository.GetProductById(id);
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
                var findShop = await _shopRepository.GetShopById(idShop);
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
                var result = await _productRepository.GetProductById(productModel.idProduct);
                if (result == null) throw new FailureRequestException(409, "id do Produto nao localizado");
                var findCategory = await _categoriesRepository.GetCategoriesById(productModel.categoriesId);
                if (findCategory == null) throw new FailureRequestException(404, "Categoria nao localizada.");
                if (productModel.imageId != null)
                {
                    var findUrlImage = await _imageRepository.GetImageById((int)productModel.imageId);
                    if (findUrlImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada.");
                    result.imageId = productModel.imageId;
                }
                var findNameProdutc = await _productRepository.GetProductByName(productModel.name, result.shopId);
                if (findNameProdutc != null) throw new FailureRequestException(409, "Produto com esse nome ja cadastrado");
                if (result.shopId != findCategory.shopId) throw new FailureRequestException(409, "Categoria informada nao pertence ao shop.");
                result.name = productModel.name;
                result.price = productModel.price;
                result.categoriesId = productModel.categoriesId;
                result.description = productModel.description;

                await _productRepository.UpdateProduct(result);
                return true;
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
