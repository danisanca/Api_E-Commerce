using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<CategoriesModel> GetByName(string name);
        Task<List<CategoriesModel>> GetAllByIds(List<Guid> ids);

    }
}
