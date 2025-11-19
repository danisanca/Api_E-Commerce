using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IShopRepository
    {
        Task<ShopModel> GetByUserId(string userId);
        Task<List<ShopModel>> GetAllByIds(List<Guid> ids);
    }
}
