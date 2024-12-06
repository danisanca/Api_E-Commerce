using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IHistoryMovimentService
    {
        Task<HistoryMovimentDto> CreateHistoryMoviment(HistoryMovimentCreateDto model);
        Task<HistoryMovimentDto> GetHistoryMovimentById(int id);
        Task<List<HistoryMovimentDto>> GetAllHistoryMovimentByProductId(int idProduct);
        Task<List<HistoryMovimentDto>> GetAllHistoryMovimentByShopId(int idShop);
    }
}
