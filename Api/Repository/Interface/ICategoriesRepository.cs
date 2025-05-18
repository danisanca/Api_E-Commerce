using ApiEstoque.Models;
using ApiEstoque.Helpers;

namespace ApiEstoque.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<List<CategoriesModel>> GetAllCategories(FilterGetRoutes status);
        Task<CategoriesModel> GetCategoriesByName(string name);

    }
}
