using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IHistoryMovimentRepository
    {
        Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByProductId(Guid idProduct);

    }
}
