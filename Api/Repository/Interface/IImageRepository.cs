using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IImageRepository
    {
        Task<List<ImageModel>> GetImagesByIdProduct(int idProduct);
        Task<ImageModel> GetImageByUrl(string url);
    }
}
