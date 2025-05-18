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

        public async Task<List<CategoriesModel>> GetAllCategories(FilterGetRoutes status)
        {
            if (status == FilterGetRoutes.Ativo) return await _db.Categories.Where(g => g.status == status.ToString()).ToListAsync();
            else if (status == FilterGetRoutes.Desabilitado) return await _db.Categories.Where(g => g.status == status.ToString()).ToListAsync();
            else return await _db.Categories.ToListAsync();
        }


        public async Task<CategoriesModel> GetCategoriesByName(string name)
        {
            return await _db.Categories.Where(x => x.name == name).FirstOrDefaultAsync();
        }

    }
}
