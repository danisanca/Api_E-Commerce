using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApiContext _db;

        public ImageRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<List<ImageModel>> GetAllByIdProduct(Guid idProduct)
        {
            return await _db.Image.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<List<ImageModel>> GetAllByProductsIds(List<Guid> ids)
        {
            return await _db.Image.Where(c => ids.Contains(c.productId))
                         .ToListAsync();
        }

        public async Task<ImageModel> GetByUrl(string url)
        {
            return await _db.Image.FirstOrDefaultAsync(x => x.url == url);
        }

    }
}
