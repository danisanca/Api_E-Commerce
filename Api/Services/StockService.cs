using ApiEstoque.Constants;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using SharedBase.Repository;

namespace ApiEstoque.Services
{
    public class StockService : IStockService
    {
        private readonly IBaseRepository<StockModel> _baseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IHistoryMovimentService _historyMovimentService;
        private readonly IMapper _mapper;

        public StockService(
            IBaseRepository<StockModel> baseRepository,
            IStockRepository stockRepository,
            IHistoryMovimentService historyMovimentService,
            IShopRepository shopRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _stockRepository = stockRepository;
            _historyMovimentService = historyMovimentService;
            _mapper = mapper;
        }

        public async Task<bool> Update(StockUpdateDto stockUpdate)
        {
            try
            {
                if (stockUpdate.amount <= 0) throw new FailureRequestException(404, "Não é possivel passar a quantidade igual a zero ou negativa.");
                var findStock = await _stockRepository.GetByProductId(stockUpdate.productId);

                if (stockUpdate.action == MovimentAction.Entrada.ToString() || 
                    stockUpdate.action == MovimentAction.Devolução.ToString())
                {
                    if (findStock == null)
                    {
                        StockModel model = await _baseRepository.InsertAsync(_mapper.Map<StockModel>(stockUpdate));
                        var stock = _mapper.Map<StockDto>(model);
                        if (stock == null) throw new FailureRequestException(404, "Houve um problema na criação do estoque.");
                        var historyModel = new HistoryMovimentCreateDto()
                        {
                            productId = stockUpdate.productId,
                            amount = stockUpdate.amount,
                            userId = stockUpdate.userId,
                            action = stockUpdate.action,
                        };
                        await _historyMovimentService.Create(historyModel);
                        return true;
                    }
                    else
                    {
                        findStock.amount += stockUpdate.amount;
                        findStock.UpdatedAt = DateTime.UtcNow;
                        var stock = await _baseRepository.UpdateAsync(findStock);
                        if (stock == false) throw new FailureRequestException(404, "Houve um problema na atualização do estoque.");
                        var historyModel = new HistoryMovimentCreateDto()
                        {
                            productId = stockUpdate.productId,
                            amount = stockUpdate.amount,
                            userId = stockUpdate.userId,
                            action = stockUpdate.action,
                        };
                        await _historyMovimentService.Create(historyModel);
                        return true;

                    }

                }
                else if (stockUpdate.action == MovimentAction.Saida.ToString() || 
                    stockUpdate.action == MovimentAction.Venda.ToString())
                {
                    findStock.amount -= stockUpdate.amount;
                    findStock.UpdatedAt = DateTime.UtcNow;
                    var stock = await _baseRepository.UpdateAsync(findStock);
                    if (stock == false) throw new FailureRequestException(404, "Houve um problema na baixa do produto no estoque.");
                    var historyModel = new HistoryMovimentCreateDto()
                    {
                        productId = stockUpdate.productId,
                        amount = stockUpdate.amount,
                        userId = stockUpdate.userId,
                        action = stockUpdate.action,
                    };
                    await _historyMovimentService.Create(historyModel);
                    return true;
                }
                else if (stockUpdate.action == MovimentAction.Acerto.ToString())
                {
                    findStock.amount = stockUpdate.amount;
                    findStock.UpdatedAt = DateTime.UtcNow;
                    var stock = await _baseRepository.UpdateAsync(findStock);
                    if (stock == false) throw new FailureRequestException(404, "Houve um problema na baixa do produto no estoque.");
                    var historyModel = new HistoryMovimentCreateDto()
                    {
                        productId = stockUpdate.productId,
                        amount = stockUpdate.amount,
                        userId = stockUpdate.userId,
                        action = stockUpdate.action,
                    };
                    await _historyMovimentService.Create(historyModel);
                    return true;
                }
                else
                {
                    throw new FailureRequestException(409, "Tipo de Movimentação Invalida");
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
        public async Task<bool> ChangeStatusById(StockChangeStatusDto model)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(model.idStock);
                if (result == null) throw new FailureRequestException(404, "Id do stock não localizado.");

                if (model.isActive == true)
                {
                    if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(404, "Stock já esta ativo.");
                    result.status = StandartStatus.Ativo.ToString();
                    var historyModel = new HistoryMovimentCreateDto()
                    {
                        productId = result.productId,
                        amount = result.amount,
                        userId = model.userId,
                        action = MovimentAction.Desbloqueio.ToString(),
                    };
                    await _historyMovimentService.Create(historyModel);
                    return await _baseRepository.UpdateAsync(result);
                }
                else
                {
                    if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(404, "Stock já esta desativado.");
                    result.status = StandartStatus.Desabilitado.ToString();
                    var historyModel = new HistoryMovimentCreateDto()
                    {
                        productId = result.productId,
                        amount = result.amount,
                        userId = model.userId,
                        action = MovimentAction.Bloqueio.ToString(),
                    };
                    await _historyMovimentService.Create(historyModel);
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
                return await _baseRepository.DeleteAsync(findStock.Id); 
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
