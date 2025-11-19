using ApiEstoque.Dto.Shop;
using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IShopService
    {
        Task<ShopDto> CreateShop(ShopCreateDto shopCreateDto);
        Task<bool> UpdateShop(ShopUpdateDto shopUpdateDto);
        Task<bool> ChangeStatusById(Guid shopId,bool isActive);
        Task<ShopDto> GetByUserId(string userId);

        //Somente em Serviços
        Task<ShopDto> GetById(Guid id);
        Task<List<ShopDto>> GetAllByIds(List<Guid> ids);
    }
}
