using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IProductRepository
    {
        Task CreateProduct(ProductModel productModel);
        Task UpdateProduct(ProductModel productModel);
        Task<List<ProductModel>> GetAllProductsByShopId(FilterGetRoutes status,int idShop);
        Task<ProductModel> GetProductById(int id);
        Task<List<ProductModel>> GetAllProductByCategoryId(int id,int idShop);
        Task<ProductModel> GetProductByName(string name, int idShop);
    }
}
