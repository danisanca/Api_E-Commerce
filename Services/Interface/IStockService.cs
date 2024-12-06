using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IStockService
    {
        Task<StockDto> CreateStock(StockCreateDto stockCreate);
        Task<bool> UpdateStock(StockUpdateDto stockUpdate);
        Task<bool> DeleteStockById(int idStock);
        Task<StockDto> GetStockById(int id);
        Task<StockDto> GetStockByProductId(int idProduct);
        Task<List<StockDto>> GetAllStockByShopId(int idShop);
    }
}
