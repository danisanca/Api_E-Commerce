using ApiEstoque.Dto.Stock;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository,IProductRepository productRepository,
            IShopRepository shopRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<StockDto> CreateStock(StockCreateDto stockCreate)
        {
            try
            {
                ProductModel findProduct = await _productRepository.GetProductById(stockCreate.productId);
                if(findProduct == null ) throw new FailureRequestException(404, "Id da produto não localizada.");
                StockModel findStock = await _stockRepository.GetStockByProductId(stockCreate.productId);
                if (findStock != null) throw new FailureRequestException(404, "Produto ja cadastrado no estoque.");
                var model = _mapper.Map<StockModel>(stockCreate);
                model.status = StandartStatus.Ativo.ToString();
                await _stockRepository.AddStock(model);
                return _mapper.Map<StockDto>(model);

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
                var findStock = await _stockRepository.GetStockById(idStock);
                if (findStock == null) throw new FailureRequestException(404, "Id do stock não localizado.");
                await _stockRepository.DeleteStock(findStock);
                return true;
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
                var findShop = await _shopRepository.GetShopById(idShop);
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
                var findStock = await _stockRepository.GetStockById(id);
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
                var findProduct = await _productRepository.GetProductById(idProduct);
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
                var findProduct = await _productRepository.GetProductById(stockUpdate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findStock = await _stockRepository.GetStockById(stockUpdate.idStock);
                if (findStock == null) throw new FailureRequestException(404, "Não há estoque para esse id.");
                if (findStock.productId != stockUpdate.productId) throw new FailureRequestException(404, "O id do produto não é o mesmo que esta cadastrado.");
                findStock.amount = stockUpdate.amount;
                await _stockRepository.UpdateStock(findStock);
                return true;
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
