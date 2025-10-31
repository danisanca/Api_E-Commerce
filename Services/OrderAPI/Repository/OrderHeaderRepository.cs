using System.Data;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.Repository.Interface;

namespace OrderAPI.Repository
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {
        private readonly ApiContext _context;

        public OrderHeaderRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(OrderHeader header)
        {
            if (header == null) return false;
            _context.OrderHeader.Add(header);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderHeader> GetById(Guid id)
        {
            return await _context.OrderHeader.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(OrderHeader header)
        {

            var result = await _context.OrderHeader.AsNoTracking().FirstOrDefaultAsync( x=>x.Id == header.Id);
            if (result == null)
                return false;

            header.UpdatedAt = DateTime.UtcNow;
            _context.Entry(result).CurrentValues.SetValues(header);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
