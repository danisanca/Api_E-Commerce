using ApiEstoque.Dto.Image;
using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IImageService
    {
        Task<ImageDto> CreateImage(ImageCreateDto image);
        Task<bool> UpdateImage(ImageUpdateDto image);
        Task<bool> ActiveImage(int id);
        Task<bool> DisableImage(int id);
        Task<List<ImageDto>> GetAllImages(FilterGetRoutes status = FilterGetRoutes.All);
        Task<ImageDto> GetImageById(int id);
        Task<ImageDto> GetImageByUrl(string url);
    }
}
