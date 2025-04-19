using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.HistoryPurchase;

namespace ApiEstoque.Services.Interface
{
    public interface IHistoryPurchaseService
    {
        Task<HistoryPurchaseDto> CreateHistoryPurchase(HistoryPurchaseCreateDto model);
        Task<HistoryPurchaseDto> GetHistoryPurchaseById(int id);
        Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByProductId(int idProduct);
        Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByShopId(int idShop);
        Task<List<HistoryPurchaseFullDto>> GetAllHistoryPurchaseByUserId(int idShop);
    }
}
