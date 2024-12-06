using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IShopRepository
    {
        Task CreateShop(ShopModel modelShop);
        Task UpdateShop(ShopModel modelShop);
        Task<List<ShopModel>> GetAllShops(FilterGetRoutes status);
        Task<ShopModel> GetShopById(int id);
        Task<ShopModel> GetShopByName(string name);
        Task<ShopModel> GetShopByUserId(int userId);
    }
}
