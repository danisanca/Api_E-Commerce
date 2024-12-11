using ApiEstoque.Models;
using ApiEstoque.Helpers;

namespace ApiEstoque.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<CategoriesModel> CreateCategories(CategoriesModel categories);
        Task<bool> UpdateCategories(CategoriesModel categories);
        Task<List<CategoriesModel>> GetAllCategories(int shopId, FilterGetRoutes status);
        Task<CategoriesModel> GetCategoriesById(int id);
        Task<CategoriesModel> GetCategoriesByName(string name, int shopId);

    }
}
