using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<AddressModel> GetAddressByUserId(int userId);
    }
}
