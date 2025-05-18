using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IShopRepository
    {
        Task<ShopModel> GetShopByName(string name);
        Task<ShopModel> GetShopByUserId(int userId);
    }
}
