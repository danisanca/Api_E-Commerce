using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class HistoryPurchaseRepository : IHistoryPurchaseRepository
    {
        private readonly ApiContext _db;

        public HistoryPurchaseRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task AddHistory(HistoryPurchaseModel model)
        {
            await _db.HistoryPurchase.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByProductId(int idProduct)
        {
            return await _db.HistoryPurchase.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByShopId(int idShop)
        {
            return await _db.HistoryPurchase.Join(
                _db.Product,
                purchase => purchase.productId,
                product => product.id,
                (purchase, product) => new
                {
                    purchase,
                    product.shopId
                }
                ).Where(j => j.shopId == idShop)
                 .Select(j => j.purchase).ToListAsync();
        }

        public async Task<HistoryPurchaseModel> GetHistoryPurchaseById(int id)
        {
            return await _db.HistoryPurchase.FirstOrDefaultAsync(x => x.id == id);
        }
    }
}
