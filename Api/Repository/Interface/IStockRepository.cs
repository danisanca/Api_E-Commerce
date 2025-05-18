using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IStockRepository
    {
        Task<StockModel> GetStockByProductId(int idProduct);
        Task<List<StockModel>> GetAllStockByShopId(int idShop);
    }
}
