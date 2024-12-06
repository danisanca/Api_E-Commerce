using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class HistoryPurchaseService : IHistoryPurchaseService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryPurchaseRepository _historyPurchaseRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public HistoryPurchaseService(IMapper mapper, IHistoryPurchaseRepository historyPurchaseRepository,
            IShopRepository shopRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _historyPurchaseRepository = historyPurchaseRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
        }

        public async Task<HistoryPurchaseDto> CreateHistoryPurchase(HistoryPurchaseCreateDto model)
        {
            try
            {
                var findProduct = await _productRepository.GetProductById(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.GetUserById(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                var history = _mapper.Map<HistoryPurchaseModel>(model);
                await _historyPurchaseRepository.AddHistory(history);
                return _mapper.Map<HistoryPurchaseDto>(history);
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

        public async Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByProductId(int idProduct)
        {
            
            try
            {
                var findProduct = await _productRepository.GetProductById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByProductId(idProduct);
                if (findHistory == null) throw new FailureRequestException(404, "Nenhum Historico localizado para o Produto");
                return _mapper.Map<List<HistoryPurchaseDto>>(findHistory);
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

        public async Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByShopId(int idShop)
        {
            
            try
            {
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop nao localizado");
                var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByShopId(idShop);
                if (findHistory == null) throw new FailureRequestException(404, "Nenhum Historico localizado para o Shop");
                return _mapper.Map<List<HistoryPurchaseDto>>(findHistory);
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

        public async Task<HistoryPurchaseDto> GetHistoryPurchaseById(int id)
        {
            
            try
            {
                var history = await _historyPurchaseRepository.GetHistoryPurchaseById(id);
                if (history == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                return _mapper.Map<HistoryPurchaseDto>(history);
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
