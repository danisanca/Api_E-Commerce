using ApiEstoque.Constants;
using ApiEstoque.Models.Base;

namespace ApiEstoque.Repository.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid item);
        Task<T> SelectByIdAsync(Guid id);
        Task<IEnumerable<T>> SelectAllByStatusAsync(
            FilterGetRoutes status = FilterGetRoutes.Ativo);
    }
}
