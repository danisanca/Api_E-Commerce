using System.Security.Policy;
using ApiEstoque.Dto.Evidence;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.Product;
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

        public async Task<EvidenceDto> CreateEvidence(EvidenceCreateDto evidenceCreate)
        {
            try
            {
                var findProduct = await _productRepository.GetProductById(evidenceCreate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.GetUserById(evidenceCreate.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                
                var model = _mapper.Map<EvidenceModel>(evidenceCreate);
                model.status = StandartStatus.Ativo.ToString();
                var result = await _evidenceRepository.addEvidence(model);
                var evidence = new EvidenceDto
                {
                    id=result.id,
                    productName = findProduct.name,
                    description = findProduct.description,
                    createdAt = result.createdAt,
                    username = findUser.username
                };
                return evidence;
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
                var findEvidence = await _evidenceRepository.GetAllEvidenceByProductId(idProduct);
                if (findEvidence == null) return new List<EvidenceDto>();

                var evidenceList = new List<EvidenceDto>();

                foreach (EvidenceModel evidence in findEvidence)
                {
                    var findUser = await _userRepository.GetUserById(evidence.userId);
                    var model = new EvidenceDto
                    {
                        id = evidence.id,
                        productName = findProduct.name,
                        description = findProduct.description,
                        createdAt = evidence.createdAt,
                        username = findUser == null ? "Anonimo" : findUser.username,
                    };
                    evidenceList.Add(model);
                }

                return evidenceList;
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
                var findEvidence = await _evidenceRepository.GetAllEvidenceByShopId(idShop);
                if (findEvidence == null) return new List<EvidenceDto>();
                var evidenceList = new List<EvidenceDto>();

                foreach (EvidenceModel evidence in findEvidence)
                {
                    var findUser = await _userRepository.GetUserById(evidence.userId);
                    var findProduct = await _productRepository.GetProductById(evidence.productId);
                    if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                    var model = new EvidenceDto
                    {
                        id = evidence.id,
                        productName = findProduct.name,
                        description = findProduct.description,
                        createdAt = evidence.createdAt,
                        username = findUser == null ? "Anonimo" : findUser.username,
                    };
                    evidenceList.Add(model);
                }

                return evidenceList;
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
                var findEvidence = await _evidenceRepository.GetEvidenceById(id);
                if (findEvidence == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                var findProduct = await _productRepository.GetProductById(findEvidence.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.GetUserById(findEvidence.userId);
                var evidence = new EvidenceDto
                {
                    id = findEvidence.id,
                    productName = findProduct.name,
                    description = findProduct.description,
                    createdAt = findEvidence.createdAt,
                    username = findUser ==null ? "Anonimo":findUser.username,
                };
                return evidence;
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
