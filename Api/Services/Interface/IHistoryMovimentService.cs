using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IHistoryMovimentService
    {
        Task<HistoryMovimentDto> CreateHistoryMoviment(HistoryMovimentCreateDto model);
        Task<List<HistoryMovimentDto>> GetAllHistoryMovimentByProductId(Guid idProduct);

        //Somente em Serviços
        Task<HistoryMovimentDto> GetHistoryMovimentById(Guid id);
    }
}
