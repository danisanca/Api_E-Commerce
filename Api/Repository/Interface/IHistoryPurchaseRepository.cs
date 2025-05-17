using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryPurchaseRepository
    {
        Task<HistoryPurchaseModel> AddHistory(HistoryPurchaseModel model);
        Task<HistoryPurchaseModel> GetHistoryPurchaseById(int id);
        Task<HistoryPurchaseModel> GetHistoryPurchaseByExternalRefId(string external_ref);
       
        Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByUserId(int idUser);
        Task<bool> UpdateHistoryPurchase(HistoryPurchaseModel model);
        Task<bool> DeleteHistoryPurchase(HistoryPurchaseModel model);
    }
}
