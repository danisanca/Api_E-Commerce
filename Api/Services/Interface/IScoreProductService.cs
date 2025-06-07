using ApiEstoque.Dto.ScoreProduct;

namespace ApiEstoque.Services.Interface
{
    public interface IScoreProductService
    {
        Task<bool> UpdateScore(ScoreProductCreateDto model);
        Task<Dictionary<string, float>> GetScoreProductByProductId(Guid idProduct);
        
    }
}
