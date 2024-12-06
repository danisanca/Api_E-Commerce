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

        public async Task CreateShop(ShopModel modelShop)
        {
            await _db.Shop.AddAsync(modelShop);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ShopModel>> GetAllShops(FilterGetRoutes status)
        {
            if (status != FilterGetRoutes.All) return await _db.Shop.Where(x => x.status == status.ToString()).ToListAsync();  
            else return await _db.Shop.ToListAsync();

        }

        public async Task<ShopModel> GetShopById(int id)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<ShopModel> GetShopByName(string name)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.name == name);
        }

        public async Task<ShopModel> GetShopByUserId(int userId)
        {
            return await _db.Shop.FirstOrDefaultAsync(x => x.userId == userId);
        }

        public async Task UpdateShop(ShopModel modelShop)
        {
            _db.Shop.Update(modelShop);
            await _db.SaveChangesAsync();
        }
    }
}
