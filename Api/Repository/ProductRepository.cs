using System.Data;
using ApiEstoque.Constants;
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

        public async Task<List<ProductModel>> GetAllByIdShop(Guid idShop, FilterGetRoutes status = FilterGetRoutes.All)
        {
            if (status == FilterGetRoutes.All)
            {
                return await _db.Product
             .Where(x => x.shopId == idShop)
             .ToListAsync();
            }
            else
            {
                return await _db.Product
             .Where(x => x.shopId == idShop && x.status == status.ToString())
             .ToListAsync();

            }
            
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

        public async Task<IEnumerable<ProductModel>> SelectAllByStatusAsync(FilterGetRoutes status = FilterGetRoutes.Ativo)
        {
            try
            {
                if (status == FilterGetRoutes.Ativo) return await _db.Product.Where(g => g.status == status.ToString()).ToListAsync();
                else if (status == FilterGetRoutes.Desabilitado) return await _db.Product.Where(g => g.status == status.ToString()).ToListAsync();
                else return await _db.Product.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
