using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace ApiEstoque.Services
{
    public class ScoreProductService : IScoreProductService
    {
        private readonly IMapper _mapper;
        private readonly IScoreProductRepository _scoreProductRepository;
        private readonly IBaseRepository<ProductModel> _productRepository;
        private readonly IBaseRepository<UserModel> _userRepository;
        private readonly IBaseRepository<ScoreProductModel> _baseRepository;
        public ScoreProductService(IMapper mapper, IScoreProductRepository scoreProductRepository,
            IBaseRepository<ProductModel> productRepository, IBaseRepository<UserModel> userRepository, IBaseRepository<ScoreProductModel> baseRepository)
        {
            _mapper = mapper;
            _scoreProductRepository = scoreProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _baseRepository = baseRepository;
        }

        
        public async Task<Dictionary<string,float>> GetScoreProductByProductId(int idProduct)
        {
            try
            {

                var findProduct = await _productRepository.SelectByIdAsync(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findScore = await _scoreProductRepository.GetAllScoreProductByProductId(idProduct);
                if (findScore == null) throw new FailureRequestException(404, "Nenhuma nota encontrada.");
                var QtdScore = findScore.Count();
                var stars = 0.0f;
                foreach (ScoreProductModel note in findScore)
                {
                    stars += note.amountStars;
                }
                var resut = stars / QtdScore;
                var starsAmont = new Dictionary<string, float>();
                starsAmont.Add("amountStars", resut);
                return starsAmont;

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateScore(ScoreProductCreateDto model)
        {
            try
            {
                var findUser = await _userRepository.SelectByIdAsync(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                var findProduct = await _productRepository.SelectByIdAsync(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");

                var listScores = await _scoreProductRepository.GetAllScoreProductByProductId(model.productId);
                if (listScores != null)
                {
                    var QtdScore = 0;
                    var stars = 0.0f;

                    foreach (ScoreProductModel score in listScores)
                    {
                        if (score.userId == model.userId)
                        {
                            score.amountStars = model.amountStars;
                            await _baseRepository.UpdateAsync(score);
                            return true;
                        }
                    }

                    ScoreProductModel newScore = _mapper.Map<ScoreProductModel>(model);
                    await _baseRepository.InsertAsync(newScore);

                    //--Calculando
                    QtdScore = listScores.Count() + 1;
                    stars = model.amountStars;
                    foreach (ScoreProductModel note in listScores)
                    {
                        stars += note.amountStars;
                    }
                    //--Resultado
                    var resut = _mapper.Map<ScoreProductDto>(newScore);
                    resut.amountStars = (stars / QtdScore);
                    return true;

                }
                else
                {
                    ScoreProductModel newScore = _mapper.Map<ScoreProductModel>(model);
                    return true;

                }

                
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
