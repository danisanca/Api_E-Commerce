using ApiEstoque.Constants;
using ApiEstoque.Data;
using ApiEstoque.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace SharedContext.Repositories.Base
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
                if (item.id == Guid.Empty)
                {
                    item.id = Guid.NewGuid();
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
        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.id.Equals(item.id));
                if (result == null)
                    return false;
                item.updatedAt = DateTime.UtcNow;
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
                var result = await _dataset.SingleOrDefaultAsync(p => p.id.Equals(id));
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
                return await _dataset.SingleOrDefaultAsync(p => p.id.Equals(id));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAllByStatusAsync(FilterGetRoutes status = FilterGetRoutes.Ativo)
        {
            try
            {
                if (status == FilterGetRoutes.Ativo) return await _dataset.Where(g => g.status == status.ToString()).ToListAsync();
                else if (status == FilterGetRoutes.Desabilitado) return await _dataset.Where(g => g.status == status.ToString()).ToListAsync();
                else return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
