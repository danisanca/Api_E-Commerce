using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IStockService
    {
        Task<bool> Update(StockUpdateDto stockUpdate);
        Task<bool> ChangeStatusById(StockChangeStatusDto model);


        //Somente em Serviços
        Task<StockDto> GetByProductId(Guid idProduct);
        Task<List<StockDto>> GetAllByProductsIds(List<Guid> ids);
        Task<StockDto> GetById(Guid id);
    }
}
