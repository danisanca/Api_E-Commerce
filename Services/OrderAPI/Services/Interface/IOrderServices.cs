using OrderAPI.Models;

namespace OrderAPI.Services.Interface
{
    public interface IOrderServices
    {
        Task<bool> Create(OrderHeader header);
        Task<bool> UpdateHeader(OrderHeader header);
        Task<OrderHeader> GetHeaderById(Guid id);
    }
}
