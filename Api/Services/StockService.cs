using ApiEstoque.Dto.Stock;
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
    public class StockService : IStockService
    {
        private readonly IBaseRepository<StockModel> _baseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IBaseRepository<ProductModel> _productRepository;
        private readonly IMapper _mapper;

        public StockService(
            IBaseRepository<StockModel> baseRepository,
            IStockRepository stockRepository,
            IBaseRepository<ProductModel> productRepository,
            IShopRepository shopRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<StockDto> CreateStock(StockCreateDto stockCreate)
        {
            try
            {
                ProductModel findProduct = await _productRepository.SelectByIdAsync(stockCreate.productId);
                if(findProduct == null ) throw new FailureRequestException(404, "Id da produto não localizada.");
                StockModel findStock = await _stockRepository.GetStockByProductId(stockCreate.productId);
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

        public async Task<bool> DeleteStockById(int idStock)
        {
            try
            {
                var findStock = await _baseRepository.SelectByIdAsync(idStock);
                if (findStock == null) throw new FailureRequestException(404, "Id do stock não localizado.");
                return await _baseRepository.DeleteAsync(findStock); 
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

        public async Task<List<StockDto>> GetAllStockByShopId(int idShop)
        {
            try
            {
                var findShop = await _baseRepository.SelectByIdAsync(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop não localizado.");
                var findStock = await _stockRepository.GetAllStockByShopId(idShop);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esse id.");
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

        public async Task<StockDto> GetStockById(int id)
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

        public async Task<StockDto> GetStockByProductId(int idProduct)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findStock = await _stockRepository.GetStockByProductId(idProduct);
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

        public async Task<bool> UpdateStock(StockUpdateDto stockUpdate)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(stockUpdate.productId);
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
    }
}
