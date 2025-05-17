using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IStockRepository
    {
        Task<StockModel> AddStock(StockModel stockModel);
        Task<bool> UpdateStock(StockModel stockModel);
        Task<bool> DeleteStock(StockModel stockModel);
        Task<StockModel> GetStockById(int id);
        Task<StockModel> GetStockByProductId(int idProduct);
        Task<List<StockModel>> GetAllStockByShopId(int idShop);
    }
}
