using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Models;
using ApiEstoque.Repository;
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
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ScoreProductService(IMapper mapper, IScoreProductRepository scoreProductRepository, 
            IProductRepository productRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _scoreProductRepository = scoreProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        
        public async Task<Dictionary<string,float>> GetScoreProductByProductId(int idProduct)
        {
            try
            {

                var findProduct = await _productRepository.GetProductById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findScore = await _scoreProductRepository.GetAllScoreProductByProductId(idProduct);
                if (findScore.IsNullOrEmpty()) throw new FailureRequestException(404, "Nenhuma nota encontrada.");
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

        public async Task<ScoreProductDto> UpdateScore(ScoreProductCreateDto model)
        {
            try
            {
                var findUser = await _userRepository.GetUserById(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                var findProduct = await _productRepository.GetProductById(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findScore = await _scoreProductRepository.GetAllScoreProductByProductId(model.productId);
                if (!findScore.IsNullOrEmpty())
                {
                    ScoreProductModel newScore = _mapper.Map<ScoreProductModel>(model);
                    await _scoreProductRepository.CreateScore(newScore);
                    //--Calculando
                    var QtdScore = findScore.Count()+1;
                    var stars = model.amountStars;
                    foreach (ScoreProductModel note in findScore)
                    {
                        stars += note.amountStars;
                    }
                    //--Resultado
                    var resut = _mapper.Map<ScoreProductDto>(newScore);
                    resut.amountStars = (stars / QtdScore);
                    return resut;
                }
                else
                {
                    ScoreProductModel newScore = _mapper.Map<ScoreProductModel>(model);
                    await _scoreProductRepository.CreateScore(newScore);
                    return _mapper.Map<ScoreProductDto>(newScore);

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
