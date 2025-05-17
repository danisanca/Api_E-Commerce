using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<AddressModel> AddAddress(AddressModel addressModel);
        Task<bool> UpdateAddress(AddressModel addressModel);
        Task<AddressModel> GetAddressByUserId(int userId);
        Task<AddressModel> GetAddressById(int id);
    }
}
