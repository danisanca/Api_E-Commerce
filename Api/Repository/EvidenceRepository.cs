using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class EvidenceRepository : IEvidenceRepository
    {
        private readonly ApiContext _db;

        public EvidenceRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<EvidenceModel> addEvidence(EvidenceModel model)
        {
            await _db.Evidence.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<bool> deleteEvidence(EvidenceModel model)
        {
             _db.Evidence.Remove(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<EvidenceModel>> GetAllEvidenceByProductId(int idProduct)
        {
            return await _db.Evidence.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<EvidenceModel>> GetAllEvidenceByShopId(int idShop)
        {
            return await _db.Evidence.Join(
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

        public async Task<EvidenceModel> GetEvidenceById(int id)
        {
            return await _db.Evidence.FirstOrDefaultAsync(x => x.id == id);
        }
    }
}
