using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryMovimentRepository
    {
        Task<HistoryMovimentModel> AddHistory(HistoryMovimentModel model);
        Task<HistoryMovimentModel> GetHistoryMovimentById(int id);
        Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByProductId(int idProduct);
        Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByShopId(int idShop);

    }
}
