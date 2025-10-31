using OrderAPI.Models;

namespace OrderAPI.Repository.Interface
{
    public interface IOrderHeaderRepository
    {
        Task<bool> Create(OrderHeader header);
        Task<bool> Update(OrderHeader header);
        Task<OrderHeader> GetById(Guid id);
    }
}
