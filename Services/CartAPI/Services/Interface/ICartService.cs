using CartAPI.Dto;
using CartAPI.Models;
using SharedBase.Dtos.Cart;
namespace CartAPI.Services.Interface
{
    public interface ICartService
    {
        Task<CartDto> GetByUserId(string userId);
        Task<CartHeaderDto> GetCartHeaderByCartDetailId(Guid cartDeatilId);
        Task<CartHeaderDto> GetCartHeaderId(Guid cartHeaderId);
        Task<CartDto> Create(CartCreateDto cartModel);
        Task<bool> Update(CartUpdateDto cartModel);
        Task<bool> Delete(Guid cartDeatilId);
        Task<bool> Clear(Guid cartHeaderId);

    }
}
