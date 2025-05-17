using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IDiscountRepository
    {
        Task<DiscountModel> AddDiscount(DiscountModel discountModel);
        Task<bool> UpdateDiscount(DiscountModel discountModel);
        Task<bool> DeleteDiscount(DiscountModel discountModel);
        Task<DiscountModel> GetDiscountByProductId(int productId);
        Task<List<DiscountModel>> GetAllDiscountsByShopId(int idShop);
    }
}
