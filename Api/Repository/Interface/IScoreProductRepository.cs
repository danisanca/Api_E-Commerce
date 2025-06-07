using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IScoreProductRepository
    {
        Task<List<ScoreProductModel>> GetAllScoreProductByProductId(Guid idProduct);
        Task<List<ScoreProductModel>> GetAllScoreProductByShopId(Guid idShop);
    }
}
