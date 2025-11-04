using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedBase.Models;

namespace SharedBase.Repository
{
    public class BaseRepository<T, TContext> : IBaseRepository<T>
     where T : BaseEntity
     where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<T> _dataset;

        public BaseRepository(TContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            if (item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();

            _dataset.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<T>> InsertRangeAsync(List<T> items)
        {
            _dataset.AddRange(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<bool> UpdateAsync(T item)
        {
            var result = await _dataset.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return false;

            item.UpdatedAt = DateTime.UtcNow;
            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> SelectByIdAsync(Guid id)
        {
            return await _dataset.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<List<T>> SelectAllByIdAsync()
        {
            return await _dataset.AsNoTracking().ToListAsync();
        }
        

    }
}
