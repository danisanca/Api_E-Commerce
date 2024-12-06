using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IScoreProductRepository
    {
        Task CreateScore(ScoreProductModel model);
        Task UpdateScore(ScoreProductModel model);
        Task<ScoreProductModel> GetScoreProductById(int id);
        Task<List<ScoreProductModel>> GetAllScoreProductByProductId(int idProduct);
        Task<List<ScoreProductModel>> GetAllScoreProductByShopId(int idShop);
    }
}
