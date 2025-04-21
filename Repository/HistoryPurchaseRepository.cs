using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using MercadoPago.Resource.User;
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

        public async Task<HistoryPurchaseModel> AddHistory(HistoryPurchaseModel model)
        {
            await _db.HistoryPurchase.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<HistoryPurchaseModel> GetHistoryPurchaseByExternalRefId(string external_ref)
        {
            return await _db.HistoryPurchase.FirstOrDefaultAsync(x => x.externalReference == external_ref);
        }

      

        public async Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByUserId(int idUser)
        {
            return await _db.HistoryPurchase.Where(x => x.userId == idUser).ToListAsync();
        }

        public async Task<HistoryPurchaseModel> GetHistoryPurchaseById(int id)
        {
            return await _db.HistoryPurchase.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<bool> UpdateHistoryPurchase(HistoryPurchaseModel model)
        {
            _db.HistoryPurchase.Update(model);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteHistoryPurchase(HistoryPurchaseModel model)
        {
            _db.HistoryPurchase.Remove(model);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
