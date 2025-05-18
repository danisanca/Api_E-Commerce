﻿using ApiEstoque.Data;
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

        public async Task<List<ImageModel>> GetImagesByIdProduct(int idProduct)
        {
            return await _db.Image.Where(x => x.productId == idProduct).ToListAsync();
        }

        public async Task<ImageModel> GetImageByUrl(string url)
        {
            return await _db.Image.FirstOrDefaultAsync(x => x.url == url);
        }

    }
}
