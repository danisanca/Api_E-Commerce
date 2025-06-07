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

        public async Task<List<EvidenceModel>> GetAllEvidenceByProductId(Guid idProduct)
        {
            return await _db.Evidence.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<EvidenceModel>> GetAllEvidenceByShopId(Guid idShop)
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

    }
}
