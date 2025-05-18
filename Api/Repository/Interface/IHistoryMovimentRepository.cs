using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryMovimentRepository
    {
        Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByProductId(int idProduct);
        Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByShopId(int idShop);

    }
}
