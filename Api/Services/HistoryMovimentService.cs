using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class HistoryMovimentService : IHistoryMovimentService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryMovimentRepository _historyMovimentRepository;
        private readonly IBaseRepository<ShopModel> _shopRepository;
        private readonly IBaseRepository<ProductModel> _productRepository;
        private readonly IBaseRepository<UserModel> _userRepository;
        private readonly IBaseRepository<HistoryMovimentModel> _baseRepository;

        public HistoryMovimentService (IMapper mapper, 
            IHistoryMovimentRepository historyMovimentRepository,
            IBaseRepository<ShopModel> shopRepository,
            IBaseRepository<ProductModel> productRepository,
            IBaseRepository<UserModel> userRepository,
            IBaseRepository<HistoryMovimentModel> _baseRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _historyMovimentRepository = historyMovimentRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
        }

        public async Task<HistoryMovimentDto> CreateHistoryMoviment(HistoryMovimentCreateDto model)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.SelectByIdAsync(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                if (model.action != MovimentAction.Entrada.ToString() && model.action != MovimentAction.Saida.ToString()
                   && model.action != MovimentAction.Acerto.ToString() && model.action != MovimentAction.Venda.ToString()) 
                    throw new FailureRequestException(404, "Tipo de Movimentação Invalida.");
                var history = _mapper.Map<HistoryMovimentModel>(model);
                
                return _mapper.Map<HistoryMovimentDto>(await _baseRepository.InsertAsync(history));
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

        public async Task<List<HistoryMovimentDto>> GetAllHistoryMovimentByProductId(int idProduct)
        {
            
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findHistory = await _historyMovimentRepository.GetAllHistoryMovimentByProductId(idProduct);
                if (findHistory == null) return new List<HistoryMovimentDto>();
                return _mapper.Map<List<HistoryMovimentDto>>(findHistory);
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

        public async Task<List<HistoryMovimentDto>> GetAllHistoryMovimentByShopId(int idShop)
        {
            
            try
            {
                var findShop = await _shopRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop nao localizado");
                var findHistory = await _historyMovimentRepository.GetAllHistoryMovimentByShopId(idShop);
                if (findHistory == null) return new List<HistoryMovimentDto>();
                return _mapper.Map<List<HistoryMovimentDto>>(findHistory);
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

        public async Task<HistoryMovimentDto> GetHistoryMovimentById(int id)
        {
           
            try
            {
                var history = await _baseRepository.SelectByIdAsync(id);
                if (history == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                return _mapper.Map<HistoryMovimentDto>(history);
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
