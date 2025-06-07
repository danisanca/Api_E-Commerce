using ApiEstoque.Dto.Categories;
using ApiEstoque.Helpers;

namespace ApiEstoque.Services.Interface
{
    public interface ICategoriesService
    {
        Task<List<CategoriesDto>> GetAll();

        //Somente em Serviços
        Task<CategoriesDto> GetByName(string name);
        Task<CategoriesDto> GetById(Guid id);
        Task<List<CategoriesDto>> GetAllByIds(List<Guid> Ids);
    }
}
