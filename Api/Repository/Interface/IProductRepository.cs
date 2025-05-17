using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IProductRepository
    {
        Task<ProductModel> CreateProduct(ProductModel productModel);
        Task<bool> UpdateProduct(ProductModel productModel);
        Task<List<ProductModel>> GetAllProductsByShopId(FilterGetRoutes status,int idShop);
        Task<List<ProductModel>> GetAllProductsActive();
        Task<ProductModel> GetProductById(int id);
        Task<List<ProductModel>> GetAllProductByCategoryId(int id,int idShop);
        Task<ProductModel> GetProductByName(string name, int idShop);
    }
}
