
using CartAPI.Models.Base;

namespace CartAPI.Repositories.Base
{
    public interface IBaseRepositorys<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<List<T>> InsertRangeAsync(List<T> item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid item);
        Task<T> SelectByIdAsync(Guid id);
    }
}
