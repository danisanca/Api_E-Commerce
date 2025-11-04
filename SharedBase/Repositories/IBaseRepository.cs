using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedBase.Models;

namespace SharedBase.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<List<T>> InsertRangeAsync(List<T> item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid item);
        Task<T> SelectByIdAsync(Guid id);
        Task<List<T>> SelectAllByIdAsync();
        
    }
}
