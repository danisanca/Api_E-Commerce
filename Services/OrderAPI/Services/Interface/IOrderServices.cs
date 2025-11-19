using OrderAPI.Dtos;
using OrderAPI.Models;

namespace OrderAPI.Services.Interface
{
    public interface IOrderServices
    {
        Task<bool> Create(OrderHeader header);
        Task<bool> UpdateHeader(OrderHeader header);
        Task<OrderHeaderDto> GetHeaderById(Guid id);
    }
}
