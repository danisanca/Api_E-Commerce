using ApiEstoque.Helpers;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllLikeName(string name);//Barra de pesquisa
        Task<List<ProductModel>> GetAllByIdShop(Guid idShop);
        Task<ProductModel> GetByNameAndIdShop(string name,Guid idShop);
    }
}
