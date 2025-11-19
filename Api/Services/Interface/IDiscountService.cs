using ApiEstoque.Dto.Discount;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IDiscountService
    {
        Task<DiscountDto> Create(DiscountCreateDto discountCreate);
        Task<bool> Update(DiscountUpdateDto discountUpdate);
        Task<bool> DeleteById(Guid id);
        Task<DiscountDto> GetByProductId(Guid idProduct);

        //Somente em Serviços
        Task<DiscountDto> GetById(Guid id);
        Task<List<DiscountDto>> GetAllByProductsIds(List<Guid> ids);
    }
}
