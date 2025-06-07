using ApiEstoque.Data;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApiContext _db;
        
        public AddressRepository(ApiContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<AddressModel> GetByUserId(string userId)
        {
            return await _db.Adress.FirstOrDefaultAsync(x => x.userId == userId);
        }

    }
}
