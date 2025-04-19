using ApiEstoque.Dto.Categories;
using ApiEstoque.Helpers;

namespace ApiEstoque.Services.Interface
{
    public interface ICategoriesService
    {
        Task<CategoriesDto> CreateCategories(CategoriesCreateDto categories);
        Task<bool> UpdateCategories(CategoriesUpdateDto categories);
        Task<bool> ActiveCategories(int idCategories);
        Task<bool> DisableCategories(int idCategories);
        Task<CategoriesDto> GetCategoriesById(int id);
        Task<CategoriesDto> GetCategoriesByName(string name);
        Task<List<CategoriesDto>> GetAllCategories(FilterGetRoutes status = FilterGetRoutes.All);
    }
}
