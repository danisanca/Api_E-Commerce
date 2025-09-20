using CartAPI.Dto;
using CartAPI.Models;

namespace CartAPI.Repositories
{
    public interface ICartRepository
    {
        Task<CartDto> GetByUserId(string userId);
        Task<int> CountCartDetailByCartHeaderId(Guid cartHeaderId);
        Task<CartDetail> GetCartDetailByProductIdAndCartHeaderId(Guid productId, Guid cartHeaderId);
        Task<List<CartDetail>> GetAllCartDetailsByCartHeaderId( Guid cartHeaderId);
    }
}
