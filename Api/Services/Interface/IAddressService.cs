using ApiEstoque.Dto.Address;
using ApiEstoque.Dto.Adress;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IAddressService
    {
        Task<AddressDto> Create(AddressCreateDto addressCreate);
        Task<bool> Update(AddressUpdateDto addressUpdate);

        //Somente em Serviços
        Task<AddressDto> GetById(Guid id);
        Task<AddressViewDto> GetByUserId(string idUser);
    }
}
