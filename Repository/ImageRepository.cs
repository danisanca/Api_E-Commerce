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

        public async Task CreateImage(ImageModel image)
        {
            await _db.Image.AddAsync(image);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ImageModel>> GetAllImages(FilterGetRoutes status)
        {
            if (status == FilterGetRoutes.Ativo) return await _db.Image.Where(g => g.status == status.ToString()).ToListAsync();
            else if (status == FilterGetRoutes.Desabilitado) return await _db.Image.Where(g => g.status == status.ToString()).ToListAsync();
            else return await _db.Image.ToListAsync();
        }

        public async Task<ImageModel> GetImageById(int id)
        {
            return await _db.Image.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<ImageModel> GetImageByUrl(string url)
        {
            return await _db.Image.FirstOrDefaultAsync(x => x.url == url);
        }

        public async Task UpdateImage(ImageModel image)
        {
            _db.Image.Update(image);
            await _db.SaveChangesAsync();
        }
    }
}
