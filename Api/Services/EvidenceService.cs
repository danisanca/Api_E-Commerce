using System.Security.Policy;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Evidence;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.Product;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoque.Services
{
    public class EvidenceService : IEvidenceService
    {
        private readonly IMapper _mapper;
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IBaseRepository<ShopModel> _shopRepository;
        private readonly IBaseRepository<ProductModel> _productRepository;
        private readonly UserManager<UserModel> _userManager;
        private readonly IBaseRepository<EvidenceModel> _baseRepository;

        public EvidenceService(IMapper mapper, 
            IEvidenceRepository evidenceRepository, IBaseRepository<ShopModel> shopRepository,
            IBaseRepository<ProductModel> productRepository, UserManager<UserModel> userManager, 
            IBaseRepository<EvidenceModel> baseRepository)
        {
            _mapper = mapper;
            _evidenceRepository = evidenceRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _baseRepository = baseRepository;
        }

        public async Task<EvidenceDto> CreateEvidence(EvidenceCreateDto evidenceCreate)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(evidenceCreate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userManager.FindByIdAsync(evidenceCreate.userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                
                var model = _mapper.Map<EvidenceModel>(evidenceCreate);
                model.status = StandartStatus.Ativo.ToString();
                var result = await _baseRepository.InsertAsync(model);
                var evidence = new EvidenceDto
                {
                    id=result.id,
                    productName = findProduct.name,
                    description = findProduct.description,
                    createdAt = result.createdAt,
                    username = findUser.UserName
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

        public async Task<bool> DeleteEvidenceById(Guid idEvidence)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(idEvidence);
                if(result == null) throw new FailureRequestException(404, "Id da evidencia nao localizada");
                return await _baseRepository.DeleteAsync(result.id);
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

        public async Task<List<EvidenceDto>> GetAllEvidenceByProductId(Guid idProduct)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findEvidence = await _evidenceRepository.GetAllEvidenceByProductId(idProduct);
                if (findEvidence == null) return new List<EvidenceDto>();

                var evidenceList = new List<EvidenceDto>();

                foreach (EvidenceModel evidence in findEvidence)
                {
                    var findUser = await _userManager.FindByIdAsync(evidence.userId.ToString());
                    var model = new EvidenceDto
                    {
                        id = evidence.id,
                        productName = findProduct.name,
                        description = findProduct.description,
                        createdAt = evidence.createdAt,
                        username = findUser == null ? "Anonimo" : findUser.UserName,
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

        public async Task<List<EvidenceDto>> GetAllEvidenceByShopId(Guid idShop)
        {
            try
            {
                var findShop = await _baseRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop nao localizado");
                var findEvidence = await _evidenceRepository.GetAllEvidenceByShopId(idShop);
                if (findEvidence == null) return new List<EvidenceDto>();
                var evidenceList = new List<EvidenceDto>();

                foreach (EvidenceModel evidence in findEvidence)
                {
                    var findUser = await _userManager.FindByIdAsync(evidence.userId.ToString());
                    var findProduct = await _productRepository.SelectByIdAsync(evidence.productId);
                    if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                    var model = new EvidenceDto
                    {
                        id = evidence.id,
                        productName = findProduct.name,
                        description = findProduct.description,
                        createdAt = evidence.createdAt,
                        username = findUser == null ? "Anonimo" : findUser.UserName,
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

        public async Task<EvidenceDto> GetEvidenceById(Guid id)
        {
            try
            {
                var findEvidence = await _baseRepository.SelectByIdAsync(id);
                if (findEvidence == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                var findProduct = await _productRepository.SelectByIdAsync(findEvidence.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userManager.FindByIdAsync(findEvidence.userId.ToString());
                var evidence = new EvidenceDto
                {
                    id = findEvidence.id,
                    productName = findProduct.name,
                    description = findProduct.description,
                    createdAt = findEvidence.createdAt,
                    username = findUser ==null ? "Anonimo":findUser.UserName,
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
