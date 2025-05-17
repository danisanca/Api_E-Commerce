using ApiEstoque.Dto.Adress;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAddress(AddressCreateDto addressCreate);
        Task<bool> UpdateAddressById(AddressUpdateDto addressUpdate);
        Task<AddressDto> GetAddressByUserId(int userId);
        Task<AddressDto> GetAddressById(int id);
    }
}
