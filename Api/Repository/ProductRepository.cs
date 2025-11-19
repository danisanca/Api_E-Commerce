using System.Collections.Generic;
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


        public async Task<List<ProductModel>> GetAllByIdShop(Guid idShop, FilterGetRoutes status = FilterGetRoutes.All, int limit = 20, int page = 0, string category = "")
        {
            if (status == FilterGetRoutes.All)
            {
                return await _db.Product.Include(c => c.categories)
             .Where(x => x.shopId == idShop && x.categories.name.ToLower() == category.ToLower()).Skip(page * limit).Take(limit)
             .ToListAsync();
            }
            else
            {
                return await _db.Product.Include(c => c.categories)
             .Where(x => x.shopId == idShop && x.status == status.ToString() && x.categories.name.ToLower() == category.ToLower()).Skip(page * limit).Take(limit)
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

        public async Task<IEnumerable<ProductModel>> SelectAllByStatusAsync( FilterGetRoutes status = FilterGetRoutes.Ativo, int limit = 20, int page = 0, string category = "")
        {
            try
            {
                if (category == "")
                {
                    if (status == FilterGetRoutes.Ativo) 
                        return await _db.Product.Where(g => g.status == status.ToString()).Skip(page * limit).Take(limit).ToListAsync();
                    else if (status == FilterGetRoutes.Desabilitado) 
                        return await _db.Product.Where(g => g.status == status.ToString()).Skip(page * limit).Take(limit).ToListAsync();
                    else 
                        return await _db.Product.Skip(page * limit).Take(limit).ToListAsync();
                }
                else
                {
                    if (status == FilterGetRoutes.Ativo)
                        return await _db.Product.Include(c=>c.categories).Where(g => g.status == status.ToString() && g.categories.name.ToLower() == category.ToLower() ).Skip(page * limit).Take(limit).ToListAsync();
                    else if (status == FilterGetRoutes.Desabilitado)
                        return await _db.Product.Include(c => c.categories).Where(g => g.status == status.ToString() && g.categories.name.ToLower() == category.ToLower()).Skip(page * limit).Take(limit).ToListAsync();
                    else
                        return await _db.Product.Include(c => c.categories).Where(g => g.categories.name.ToLower() == category.ToLower()).Skip(page * limit).Take(limit).ToListAsync();
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountProducts(FilterGetRoutes status = FilterGetRoutes.Ativo, string category = "")
        {
            try
            {
                if (category != "")
                {
                    if (status == FilterGetRoutes.Ativo) return _db.Product.Include(c => c.categories).Where(g => g.status == status.ToString() && g.categories.name.ToLower() == category.ToLower()).Count();
                    else if (status == FilterGetRoutes.Desabilitado) return _db.Product.Include(c => c.categories).Where(g => g.status == status.ToString() && g.categories.name.ToLower() == category.ToLower()).Count();
                    else return _db.Product.Include(c => c.categories).Where(g => g.categories.name.ToLower() == category.ToLower()).Count();
                }
                else
                {
                    if (status == FilterGetRoutes.Ativo) return _db.Product.Where(g => g.status == status.ToString()).Count();
                    else if (status == FilterGetRoutes.Desabilitado) return _db.Product.Where(g => g.status == status.ToString()).Count();
                    else return _db.Product.Count();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
