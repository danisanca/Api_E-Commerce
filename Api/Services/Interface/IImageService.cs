using ApiEstoque.Dto.Image;
using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IImageService
    {
        Task<ImageDto> Create(ImageCreateDto image);
        Task<bool> Delete(Guid id);
        Task<ImageDto> GetById(Guid id);

        //Somente em Serviços
        Task<ImageDto> GetByUrl(string url);
        Task<List<string>> GetAllByIdProduct(Guid idProduct);
        Task<List<ImageDto>> GetAllByProductsIds(List<Guid> ids);
        Task<bool> DeleteByUrl(string url);
    }
}
