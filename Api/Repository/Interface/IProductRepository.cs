using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProductsByShopId(FilterGetRoutes status,int idShop);
        Task<List<ProductModel>> GetAllProductsOnStock();
        Task<List<ProductModel>> GetAllProductByCategoryId(int id,int idShop);
        Task<ProductModel> GetProductByName(string name, int idShop);
    }
}
