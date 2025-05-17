using ApiEstoque.Dto.Product;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using System.Net.NetworkInformation;

namespace ApiEstoque.Services.Interface
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(ProductCreateDto productModel);
        Task<bool> UpdateProduct(ProductUpdateDto productModel);
        Task<bool> ActiveProduct(int idProduct);
        Task<bool> DisableProduct(int idProduct);
        Task<List<ProductDto>> GetAllProductsByShopId(int idShop,FilterGetRoutes status = FilterGetRoutes.All);
        Task<ProductDto> GetProductById(int id);
        Task<List<ProductDto>> GetAllProductByCategoryId(int idCategory, int idShop);
        Task<ProductDto> GetProductByName(string name, int idShop);
        Task<ProductFullDto> GetProductFullById(int id);
        Task<ProductFullDto> GetProductFullByName(string name, int idShop);
        Task<List<ProductFullDto>> GetAllProductsFullActive();
    }
}
