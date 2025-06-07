using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IStockService
    {
        Task<StockDto> Create(StockCreateDto stockCreate);
        Task<bool> Update(StockUpdateDto stockUpdate);
        Task<bool> Delete(Guid idStock);
        Task<bool> ChangeStatusById(Guid idStock,bool isActive);


        //Somente em Serviços
        Task<StockDto> GetByProductId(Guid idProduct);
        Task<List<StockDto>> GetAllByProductsIds(List<Guid> ids);
        Task<StockDto> GetById(Guid id);
    }
}
