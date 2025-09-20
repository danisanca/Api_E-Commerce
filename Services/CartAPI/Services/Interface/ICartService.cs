using CartAPI.Dto;
using CartAPI.Models;

namespace CartAPI.Services.Interface
{
    public interface ICartService
    {
        Task<CartDto> GetByUserId(string userId);
        Task<CartDto> Create(CartCreateDto cartModel);
        Task<bool> Update(CartUpdateDto cartModel);
        Task<bool> Delete(Guid cartDeatilId);
        Task<bool> Clear(Guid cartHeaderId);

    }
}
