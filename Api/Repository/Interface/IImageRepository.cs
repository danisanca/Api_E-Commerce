using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IImageRepository
    {
        Task<List<ImageModel>> GetAllByIdProduct(Guid idProduct);
        Task<ImageModel> GetByUrl(string url);
        Task<List<ImageModel>> GetAllByProductsIds(List<Guid> ids);
    }
}
