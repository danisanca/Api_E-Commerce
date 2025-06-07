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
            return await _db.Categories.Where(c => ids.Contains(c.id))
                         .ToListAsync();
        }

        public async Task<CategoriesModel> GetByName(string name)
        {
            return await _db.Categories.Where(x => x.name == name).FirstOrDefaultAsync();
        }

    }
}
