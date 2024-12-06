using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryPurchaseRepository
    {
        Task AddHistory(HistoryPurchaseModel model);
        Task<HistoryPurchaseModel> GetHistoryPurchaseById(int id);
        Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByProductId(int idProduct);
        Task<List<HistoryPurchaseModel>> GetAllHistoryPurchaseByShopId(int idShop);
    }
}
