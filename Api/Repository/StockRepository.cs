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

        public async Task<StockModel> AddStock(StockModel stockModel)
        {
            await _db.Stock.AddAsync(stockModel);
            await _db.SaveChangesAsync();
            return stockModel;
        }

        public async Task<StockModel> GetStockByProductId(int idProduct)
        {
            return await _db.Stock.FirstOrDefaultAsync(x => x.productId == idProduct);
        }

        public async Task<List<StockModel>> GetAllStockByShopId(int idShop)
        {
            return await _db.Stock.Join(
                    _db.Product,
                    stock => stock.productId,
                    product => product.id,
                    (stock, product) => new
                    {
                        stock,
                        product.shopId
                    }
                ).Where(joined => joined.shopId == idShop)
                .Select(joined => joined.stock).ToListAsync();
        }

        public async Task<StockModel> GetStockById(int id)
        {
            return await _db.Stock.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<bool> UpdateStock(StockModel stockModel)
        {
            _db.Stock.Update(stockModel);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStock(StockModel stockModel)
        {
            _db.Stock.Remove(stockModel);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
