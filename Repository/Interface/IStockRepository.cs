using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IStockRepository
    {
        Task AddStock(StockModel stockModel);
        Task UpdateStock(StockModel stockModel);
        Task DeleteStock(StockModel stockModel);
        Task<StockModel> GetStockById(int id);
        Task<StockModel> GetStockByProductId(int idProduct);
        Task<List<StockModel>> GetAllStockByShopId(int idShop);
    }
}
