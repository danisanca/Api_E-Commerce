using ApiEstoque.Constants;
using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllLikeName(string name);//Barra de pesquisa
        Task<List<ProductModel>> GetAllByIdShop(Guid idShop, FilterGetRoutes status = FilterGetRoutes.All, int limit = 20, int page = 0, string category = "");
        Task<ProductModel> GetByNameAndIdShop(string name,Guid idShop);
        Task<IEnumerable<ProductModel>> SelectAllByStatusAsync(FilterGetRoutes status = FilterGetRoutes.Ativo,int limit = 20, int page = 0, string category = "");
        Task<int> CountProducts(FilterGetRoutes status = FilterGetRoutes.Ativo, string category = "");
    }
}
