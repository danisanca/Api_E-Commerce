using ApiEstoque.Dto.Discount;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IDiscountService
    {
        Task<DiscountDto> CreateDiscount(DiscountCreateDto discountCreate);
        Task<DiscountDto> GetDiscountByProductId(int idProduct);
        Task<bool> UpdateDiscountByProductId(DiscountUpdateDto discountUpdate);
        Task<bool> DeleteDiscountByProductId(int idProduct);
        Task<List<DiscountDto>> GetAllDiscountsByShopId(int idShop);
    }
}
