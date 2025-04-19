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

        public async Task<AddressModel> AddAddress(AddressModel addressModel)
        {
            await _db.Adress.AddAsync(addressModel);
            await _db.SaveChangesAsync();
            return addressModel;
        }

        public async Task<AddressModel> GetAddressById(int id)
        {
            return await _db.Adress.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<AddressModel> GetAddressByUserId(int userId)
        {
            return await _db.Adress.FirstOrDefaultAsync(x => x.userId == userId);
        }

        public async Task<bool> UpdateAddress(AddressModel addressModel)
        {
            _db.Adress.Update(addressModel);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
