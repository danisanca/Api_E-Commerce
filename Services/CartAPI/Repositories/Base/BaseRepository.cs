using CartAPI.Data;
using CartAPI.Models;
using CartAPI.Models.Base;
using CartAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApiContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(ApiContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                _dataset.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }
        public async Task<List<T>> InsertRangeAsync(List<T> cartList)
        {
            try
            {
                _dataset.AddRange(cartList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cartList;
        }
        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                    return false;
                item.UpdatedAt = DateTime.UtcNow;
                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                    return false;
                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> SelectByIdAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        
    }
}
