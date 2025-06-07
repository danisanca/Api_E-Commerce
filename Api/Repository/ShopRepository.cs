using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApiContext _db;

        public ShopRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<ShopModel> GetByUserId(string userId)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.userId == userId);
        }
        public async Task<List<ShopModel>> GetAllByIds(List<Guid> ids)
        {
            return await _db.Shop.Where(c => ids.Contains(c.id))
                         .ToListAsync();
        }
    }
}
