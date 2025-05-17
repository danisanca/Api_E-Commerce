using ApiEstoque.Dto.Shop;
using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IShopService
    {
        Task<ShopDto> CreateShop(ShopCreateDto shopCreateDto);
        Task<bool> UpdateShop(ShopUpdateDto shopUpdateDto);
        Task<bool> ActiveShop(int shopId);
        Task<bool> DisableShop(int shopId);
        Task<List<ShopDto>> GetAllShops(FilterGetRoutes status = FilterGetRoutes.All);
        Task<ShopDto> GetShopById(int id);
        Task<ShopDto> GetShopByUserId(int userId);
        Task<ShopDto> GetShopByName(string name);
    }
}
