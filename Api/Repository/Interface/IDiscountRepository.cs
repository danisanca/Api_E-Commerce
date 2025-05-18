using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IDiscountRepository
    {
        Task<DiscountModel> GetDiscountByProductId(int productId);
        Task<List<DiscountModel>> GetAllDiscountsByShopId(int idShop);
    }
}
