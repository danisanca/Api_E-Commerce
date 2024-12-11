using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class ScoreProductRepository : IScoreProductRepository
    {
        private readonly ApiContext _db;

        public ScoreProductRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<ScoreProductModel> CreateScore(ScoreProductModel model)
        {
            await _db.ScoreProduct.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<List<ScoreProductModel>> GetAllScoreProductByProductId(int idProduct)
        {
            return await _db.ScoreProduct.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<ScoreProductModel>> GetAllScoreProductByShopId(int idShop)
        {
            return await _db.ScoreProduct.Join(
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

        public async Task<ScoreProductModel> GetScoreProductById(int id)
        {
            return await _db.ScoreProduct.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<bool> UpdateScore(ScoreProductModel model)
        {
            _db.ScoreProduct.Update(model);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
