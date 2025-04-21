using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Dto.PaymentRequest;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IHistoryPurchaseService
    {
        Task<HistoryPurchaseDto> CreatePendingPurchase(PaymentRequestDto model,string ext_ref);
        Task<HistoryPurchaseDto> GetHistoryPurchaseById(int id);
        Task<HistoryPurchaseDto> GetHistoryPurchaseByExternalRefId(string external_ref);
        Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByUserId(int idUser);
        Task<bool> UpdateHistoryPurchaseByExternalRef(string external_ref, string status);

    }
}
