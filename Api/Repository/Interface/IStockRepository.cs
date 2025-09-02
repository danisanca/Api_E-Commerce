using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IStockRepository
    {
        Task<StockModel> GetByProductId(Guid idProduct);
        Task<StockModel> GetByProductAndShop(Guid idProduct, Guid idShop);
        Task<List<StockModel>> GetAllByProductsIds(List<Guid> ids);
    }
}
