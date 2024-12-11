using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IImageRepository
    {
        Task<ImageModel> CreateImage(ImageModel image);
        Task<bool> UpdateImage(ImageModel image);
        Task<List<ImageModel>> GetAllImages(FilterGetRoutes status);
        Task<ImageModel> GetImageById(int id);
        Task<ImageModel> GetImageByUrl(string url);
    }
}
