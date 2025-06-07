using System.Data;
using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiContext _db;

        public ProductRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<List<ProductModel>> GetAllByIdShop(Guid idShop)
        {
            return await _db.Product
         .Where(x => x.shopId == idShop)
         .ToListAsync();
        }

        public async Task<List<ProductModel>> GetAllLikeName(string name)
        {
            return await _db.Product
         .Where(x => x.name.Contains(name))
         .ToListAsync();
        }

        public async Task<ProductModel> GetByNameAndIdShop(string name, Guid idShop)
        {
            return await _db.Product.SingleOrDefaultAsync(p => p.name == name && p.shopId == idShop);
        }
    }
}
