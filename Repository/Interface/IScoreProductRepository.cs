using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IScoreProductRepository
    {
        Task<ScoreProductModel> CreateScore(ScoreProductModel model);
        Task<bool> UpdateScore(ScoreProductModel model);
        Task<ScoreProductModel> GetScoreProductById(int id);
        Task<List<ScoreProductModel>> GetAllScoreProductByProductId(int idProduct);
        Task<List<ScoreProductModel>> GetAllScoreProductByShopId(int idShop);
    }
}
