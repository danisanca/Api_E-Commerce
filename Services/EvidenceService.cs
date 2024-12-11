using ApiEstoque.Dto.Evidence;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class EvidenceService : IEvidenceService
    {
        private readonly IMapper _mapper;
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public EvidenceService(IMapper mapper, IEvidenceRepository evidenceRepository, IShopRepository shopRepository,
            IProductRepository productRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _evidenceRepository = evidenceRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<EvidenceDto> CreateEvidence(EvidenceCreateDto model)
        {
            try
            {
                var findProduct = await _productRepository.GetProductById(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.GetUserById(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                
                var evidence = _mapper.Map<EvidenceModel>(model);
                evidence.status = StandartStatus.Ativo.ToString();
                return _mapper.Map<EvidenceDto>(await _evidenceRepository.addEvidence(evidence));
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

        public async Task<bool> DeleteEvidenceById(int idEvidence)
        {
            try
            {
                var result = await _evidenceRepository.GetEvidenceById(idEvidence);
                if(result == null) throw new FailureRequestException(404, "Id da evidencia nao localizada");
                return await _evidenceRepository.deleteEvidence(result);
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

        public async Task<List<EvidenceDto>> GetAllEvidenceByProductId(int idProduct)
        {
            try
            {
                var findProduct = await _productRepository.GetProductById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findEvidende = await _evidenceRepository.GetAllEvidenceByProductId(idProduct);
                if (findEvidende == null) return new List<EvidenceDto>();
                return _mapper.Map<List<EvidenceDto>>(findEvidende);
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

        public async Task<List<EvidenceDto>> GetAllEvidenceByShopId(int idShop)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop nao localizado");
                var findEvidende = await _evidenceRepository.GetAllEvidenceByShopId(idShop);
                if (findEvidende == null) return new List<EvidenceDto>();
                return _mapper.Map<List<EvidenceDto>>(findEvidende);
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

        public async Task<EvidenceDto> GetEvidenceById(int id)
        {
            try
            {
                var evidence = await _evidenceRepository.GetEvidenceById(id);
                if (evidence == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                return _mapper.Map<EvidenceDto>(evidence);
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
