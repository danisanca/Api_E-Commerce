using System.Data;
using ApiEstoque.Constants;
using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApiContext _db;

        public CategoriesRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<List<CategoriesModel>> GetAllByIds(List<Guid> ids)
        {
            return await _db.Categories.Where(c => ids.Contains(c.Id))
                         .ToListAsync();
        }

        public async Task<CategoriesModel> GetByName(string name)
        {
            return await _db.Categories.Where(x => x.name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CategoriesModel>> SelectAllByStatusAsync(FilterGetRoutes status = FilterGetRoutes.Ativo)
        {
            try
            {
                if (status == FilterGetRoutes.Ativo) return await _db.Categories.Where(g => g.status == status.ToString()).ToListAsync();
                else if (status == FilterGetRoutes.Desabilitado) return await _db.Categories.Where(g => g.status == status.ToString()).ToListAsync();
                else return await _db.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
