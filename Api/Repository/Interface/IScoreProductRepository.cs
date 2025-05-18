using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IScoreProductRepository
    {
        Task<List<ScoreProductModel>> GetAllScoreProductByProductId(int idProduct);
        Task<List<ScoreProductModel>> GetAllScoreProductByShopId(int idShop);
    }
}
