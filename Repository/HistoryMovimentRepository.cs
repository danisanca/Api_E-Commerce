using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class HistoryMovimentRepository : IHistoryMovimentRepository
    {
        private readonly ApiContext _db;

        public HistoryMovimentRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<HistoryMovimentModel> AddHistory(HistoryMovimentModel model)
        {
            await _db.HistoryMoviment.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByProductId(int idProduct)
        {
            return await _db.HistoryMoviment.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByShopId(int idShop)
        {
            return await _db.HistoryMoviment.Join(
                _db.Product,
                stock => stock.productId,
                product => product.id,
                (stock, product) => new
                {
                    stock,
                    product.shopId
                }
                ).Where(j => j.shopId == idShop)
                 .Select(j => j.stock).ToListAsync();
        }

        public async Task<HistoryMovimentModel> GetHistoryMovimentById(int id)
        {
            return await _db.HistoryMoviment.FirstOrDefaultAsync(x => x.id == id);
        }
    }
}
