using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApiContext _db;

        public StockRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<StockModel> GetByProductId(Guid idProduct)
        {
            return await _db.Stock.FirstOrDefaultAsync(x => x.productId == idProduct);
        }

        public async Task<List<StockModel>> GetAllByProductsIds(List<Guid> ids)
        {
            return await _db.Stock.Where(c => ids.Contains(c.productId))
                        .ToListAsync();
        }

        public async Task<StockModel> GetByProductAndShop(Guid idProduct, Guid idShop)
        {
            return await _db.Stock.FirstOrDefaultAsync(x => x.productId == idProduct && x.shopId == idShop);
        }
    }
}
