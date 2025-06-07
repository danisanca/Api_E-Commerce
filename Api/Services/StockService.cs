using ApiEstoque.Constants;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class StockService : IStockService
    {
        private readonly IBaseRepository<StockModel> _baseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        public StockService(
            IBaseRepository<StockModel> baseRepository,
            IStockRepository stockRepository,
            IProductService productService,
            IShopService shopService,
            IShopRepository shopRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _stockRepository = stockRepository;
            _productService = productService;
            _mapper = mapper;
            _shopService = shopService;
        }

        public async Task<StockDto> Create(StockCreateDto stockCreate)
        {
            try
            {
                var findProduct = await _productService.GetById(stockCreate.productId);
                if(findProduct == null ) throw new FailureRequestException(404, "Id da produto não localizada.");
                StockModel findStock = await _stockRepository.GetByProductId(stockCreate.productId);
                if (findStock != null) throw new FailureRequestException(409, "Produto ja cadastrado no estoque.");
                var model = _mapper.Map<StockModel>(stockCreate);
                model.status = StandartStatus.Ativo.ToString();
                return _mapper.Map<StockDto>(await _baseRepository.InsertAsync(model));

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
        public async Task<bool> ChangeStatusById(Guid idStock, bool isActive)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(idStock);
                if (result == null) throw new FailureRequestException(404, "Id do stock não localizado.");

                if (isActive == true)
                {
                    if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(404, "Stock já esta ativo.");
                    result.status = StandartStatus.Ativo.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }
                else
                {
                    if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(404, "Stock já esta desativado.");
                    result.status = StandartStatus.Desabilitado.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(Guid idStock)
        {
            try
            {
                var findStock = await _baseRepository.SelectByIdAsync(idStock);
                if (findStock == null) throw new FailureRequestException(404, "Id do stock não localizado.");
                return await _baseRepository.DeleteAsync(findStock.id); 
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

      

        public async Task<StockDto> GetById(Guid id)
        {
            try
            {
                var findStock = await _baseRepository.SelectByIdAsync(id);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esse id.");
                return _mapper.Map<StockDto>(findStock);
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

        public async Task<StockDto> GetByProductId(Guid idProduct)
        {
            try
            {
                var findProduct = await _productService.GetById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findStock = await _stockRepository.GetByProductId(idProduct);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esse id.");
                return _mapper.Map<StockDto>(findStock);
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

        public async Task<bool> Update(StockUpdateDto stockUpdate)
        {
            try
            {
                var findProduct = await _productService.GetById(stockUpdate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findStock = await _baseRepository.SelectByIdAsync(stockUpdate.idStock);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esse id.");
                if (findStock.productId != stockUpdate.productId) throw new FailureRequestException(409, "O id do produto não é o mesmo que esta cadastrado.");
                findStock.amount = stockUpdate.amount;
                return await _baseRepository.UpdateAsync(findStock); 
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

        public async Task<List<StockDto>> GetAllByProductsIds(List<Guid> ids)
        {
            try
            {
                var findStock = await _stockRepository.GetAllByProductsIds(ids);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esses ids.");
                return _mapper.Map<List<StockDto>>(findStock);
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
