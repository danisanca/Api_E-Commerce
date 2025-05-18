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


        public async Task<List<ProductModel>> GetAllProductByCategoryId(int id, int idShop)
        {
            return await _db.Product.Where(x => x.categoriesId == id && x.shopId == idShop).ToListAsync();
        }

        public async Task<List<ProductModel>> GetAllProductsOnStock()
        {
            var query = from product in _db.Product
                        join stock in _db.Stock on product.id equals stock.productId
                        where product.status == "Ativo"
                        select product;

            return await query.Distinct().ToListAsync();
        }

        public async Task<List<ProductModel>> GetAllProductsByShopId(FilterGetRoutes status, int idShop)
        {
            if (status != FilterGetRoutes.All) return await _db.Product.Where(x => x.status == status.ToString() && x.shopId == idShop).ToListAsync();
            else return await _db.Product.Where(x => x.shopId == idShop).ToListAsync();
        }

        public async Task<ProductModel> GetProductByName(string name, int idShop)
        {
            return await _db.Product.Where(x => x.name == name && x.shopId == idShop).FirstOrDefaultAsync();
        }

    }
}
