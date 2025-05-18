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

        public async Task<ShopModel> GetShopByName(string name)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.name == name);
        }

        public async Task<ShopModel> GetShopByUserId(int userId)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.userId == userId);
        }
    }
}
