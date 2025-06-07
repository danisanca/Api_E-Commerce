using ApiEstoque.Dto.Adress;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IAddressService
    {
        Task<AddressDto> Create(AddressCreateDto addressCreate);
        Task<bool> Update(AddressUpdateDto addressUpdate);
        Task<AddressDto> GetByUserId(string userId);

        //Somente em Serviços
        Task<AddressDto> GetById(Guid id);
    }
}
