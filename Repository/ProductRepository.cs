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

        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
            await _db.Product.AddAsync(productModel);
            await _db.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<ProductModel>> GetAllProductByCategoryId(int id, int idShop)
        {
            return await _db.Product.Where(x => x.categoriesId == id && x.shopId == idShop).ToListAsync();
        }

        public async Task<List<ProductModel>> GetAllProductsByShopId(FilterGetRoutes status, int idShop)
        {
            if (status != FilterGetRoutes.All) return await _db.Product.Where(x => x.status == status.ToString() && x.shopId == idShop).ToListAsync();
            else return await _db.Product.Where(x => x.shopId == idShop).ToListAsync();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            return await _db.Product.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<ProductModel> GetProductByName(string name, int idShop)
        {
            return await _db.Product.Where(x => x.name == name && x.shopId == idShop).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(ProductModel productModel)
        {
            _db.Product.Update(productModel);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
