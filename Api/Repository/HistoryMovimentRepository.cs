using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class HistoryMovimentRepository : IHistoryMovimentRepository
    {
        private readonly ApiContext _db;

        public HistoryMovimentRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<List<HistoryMovimentModel>> GetAllHistoryMovimentByProductId(Guid idProduct)
        {
            return await _db.HistoryMoviment.Where(x => x.productId == idProduct).ToListAsync();
        }

    }
}
