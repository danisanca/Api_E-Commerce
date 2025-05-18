using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryPurchaseRepository
    {
        Task<HistoryPurchaseModel> GetHistoryPurchaseByExternalRefId(string external_ref);
        Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByUserId(int idUser);
        
    }
}
