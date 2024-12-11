using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using ApiEstoque.Helpers;
using System.Net.NetworkInformation;

namespace ApiEstoque.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApiContext _db;

        public CategoriesRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<CategoriesModel> CreateCategories(CategoriesModel categories)
        {
            await _db.Categories.AddAsync(categories);
            await _db.SaveChangesAsync();
            return categories;
        }

        public async Task<List<CategoriesModel>> GetAllCategories(int shopId, FilterGetRoutes status)
        {
            if (status == FilterGetRoutes.Ativo) return await _db.Categories.Where(g => g.status == status.ToString() && g.shopId == shopId).ToListAsync();
            else if (status == FilterGetRoutes.Desabilitado) return await _db.Categories.Where(g => g.status == status.ToString() && g.shopId == shopId).ToListAsync();
            else return await _db.Categories.Where(g => g.shopId == shopId).ToListAsync();
        }

        public async Task<CategoriesModel> GetCategoriesById(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<CategoriesModel> GetCategoriesByName(string name, int shopId)
        {
            return await _db.Categories.Where(x => x.name == name && x.shopId == shopId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCategories(CategoriesModel categories)
        {
            _db.Categories.Update(categories);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
