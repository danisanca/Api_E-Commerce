using System.Text.Json;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Dto.PaymentRequest;
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
    public class HistoryPurchaseService : IHistoryPurchaseService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryPurchaseRepository _historyPurchaseRepository;
        private readonly IBaseRepository<UserModel> _userRepository;
        private readonly IBaseRepository<HistoryPurchaseModel> _baseRepository;

        public HistoryPurchaseService(IMapper mapper,
            IHistoryPurchaseRepository historyPurchaseRepository,
            IBaseRepository<UserModel> userRepository, 
            IBaseRepository<HistoryPurchaseModel> baseRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _historyPurchaseRepository = historyPurchaseRepository;
            _baseRepository = baseRepository;
        }

        public async Task<HistoryPurchaseDto> CreatePendingPurchase(PaymentRequestDto model, string ext_ref)
        {
            try
            {
                var findUser = await _userRepository.SelectByIdAsync(model.UserId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                if (model.CartList.Count() <=0) throw new FailureRequestException(404, "Lista de Produtos vazia");
                var history = new HistoryPurchaseModel
                {
                    cartProducts = JsonSerializer.Serialize(model.CartList),
                    price = model.finalPrice,
                    userId = model.UserId,
                    externalReference = ext_ref,
                    status = "Pending",
                    createdAt = DateTime.Now,
                };
                var createModel = await _baseRepository.InsertAsync(history);
                var result = new HistoryPurchaseDto
                {
                    id= createModel.id,
                    cartProducts = JsonSerializer.Deserialize<List<CartItemDTO>>(createModel.cartProducts),
                    price = createModel.price,
                    userId = model.UserId,
                    externalReference = createModel.externalReference,
                    status = createModel.status,
                    createdAt = DateTime.Now,
                };
                return result;


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
        public async Task<bool> UpdateHistoryPurchaseByExternalRef(string external_ref, string status)
        {
            //pending
            try
            {
                var history = await _historyPurchaseRepository.GetHistoryPurchaseByExternalRefId(external_ref);
                if (history == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                if (status == "approved")
                {
                    history.status = status;
                    await _baseRepository.UpdateAsync(history);
                    return true;
                }
                else if (status == "rejected")
                {
                    await _baseRepository.DeleteAsync(history);
                    return true;
                }
                else
                {
                    return false;
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
        public async Task<HistoryPurchaseDto> GetHistoryPurchaseById(int id)
        {
            
            try
            {
               
                var findHistory = await _baseRepository.SelectByIdAsync(id);
                if (findHistory == null) return new HistoryPurchaseDto();
                var result = new HistoryPurchaseDto
                {
                    id = findHistory.id,
                    cartProducts = JsonSerializer.Deserialize<List<CartItemDTO>>(findHistory.cartProducts),
                    price = findHistory.price,
                    userId = findHistory.userId,
                    externalReference = findHistory.externalReference,
                    status = findHistory.status,
                    createdAt = findHistory.createdAt,
                };
                return result;
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

        public async Task<HistoryPurchaseDto> GetHistoryPurchaseByExternalRefId(string external_ref)
        {
            try
            {

                var findHistory = await _historyPurchaseRepository.GetHistoryPurchaseByExternalRefId(external_ref);
                if (findHistory == null) return new HistoryPurchaseDto();
                var result = new HistoryPurchaseDto
                {
                    id = findHistory.id,
                    cartProducts = JsonSerializer.Deserialize<List<CartItemDTO>>(findHistory.cartProducts),
                    price = findHistory.price,
                    userId = findHistory.userId,
                    externalReference = findHistory.externalReference,
                    status = findHistory.status,
                    createdAt = findHistory.createdAt,
                };
                return result;
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

        public async Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByUserId(int idUser)
        {
            try
            {

                var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByUserId(idUser);
                if (findHistory == null) return new List<HistoryPurchaseDto>();
                var listPurchase = new List<HistoryPurchaseDto>();

                foreach (var item in findHistory)
                {
                    var result = new HistoryPurchaseDto
                    {
                        id = item.id,
                        cartProducts = JsonSerializer.Deserialize<List<CartItemDTO>>(item.cartProducts),
                        price = item.price,
                        userId = item.userId,
                        externalReference = item.externalReference,
                        status = item.status,
                        createdAt = item.createdAt,
                    };
                    listPurchase.Add(result);
                }
               
                return listPurchase;
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
