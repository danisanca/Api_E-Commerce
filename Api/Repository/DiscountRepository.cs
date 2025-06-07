using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly ApiContext _db;

        public DiscountRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<List<DiscountModel>> GetAllByProductsIds(List<Guid> ids)
        {
            return await _db.Discount.Where(c => ids.Contains(c.productId))
                        .ToListAsync();
        }

        public async Task<DiscountModel> GetByProductId(Guid productId)
        {
            return await _db.Discount.FirstOrDefaultAsync(x => x.productId == productId);
        }

    }
}
