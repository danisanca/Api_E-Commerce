using ApiEstoque.Dto.ScoreProduct;

namespace ApiEstoque.Services.Interface
{
    public interface IScoreProductService
    {
        Task<ScoreProductDto> UpdateScore(ScoreProductCreateDto model);
        Task<Dictionary<string, float>> GetScoreProductByProductId(int idProduct);
        
    }
}
