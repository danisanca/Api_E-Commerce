using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IDiscountRepository
    {
        Task<DiscountModel> GetByProductId(Guid productId);
        Task<List<DiscountModel>> GetAllByProductsIds(List<Guid> ids);
    }
}
