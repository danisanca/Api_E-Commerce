using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly ApiContext _db;

        public DiscountRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<DiscountModel> AddDiscount(DiscountModel discountModel)
        {
            await _db.Discount.AddAsync(discountModel);
            await _db.SaveChangesAsync();
            return discountModel;
        }

        public async Task<bool> DeleteDiscount(DiscountModel discountModel)
        {
            _db.Discount.Remove(discountModel);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<DiscountModel>> GetAllDiscountsByShopId(int idShop)
        {
            return await _db.Discount
            .Where(d => _db.Product
                .Any(p => p.id == d.productId && p.shopId == idShop))
            .ToListAsync();
        }

        public async Task<DiscountModel> GetDiscountByProductId(int productId)
        {
            return await _db.Discount.FirstOrDefaultAsync(x => x.productId == productId);
        }

        public async Task<bool> UpdateDiscount(DiscountModel discountModel)
        {
            _db.Discount.Update(discountModel);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
