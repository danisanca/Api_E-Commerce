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
    }
}
