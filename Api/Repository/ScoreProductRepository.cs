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


        public async Task<List<ScoreProductModel>> GetAllScoreProductByProductId(Guid idProduct)
        {
            return await _db.ScoreProduct.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<ScoreProductModel>> GetAllScoreProductByShopId(Guid idShop)
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

    }
}
