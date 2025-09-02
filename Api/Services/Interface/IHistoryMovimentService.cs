using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IHistoryMovimentService
    {
        Task<HistoryMovimentDto> Create(HistoryMovimentCreateDto model);
        Task<List<HistoryMovimentDto>> GetAllByProductId(Guid idProduct);

        //Somente em Serviços
        Task<HistoryMovimentDto> GetById(Guid id);
    }
}
