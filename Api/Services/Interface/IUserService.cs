using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoque.Services.Interface
{
    public interface IUserService
    {
        Task<bool> Create(UserCreateDto userCreateDto);
        Task<bool> Update(UserUpdateDto userUpdateDto);
        Task<IdentityResult> ChangePassword(ChangePasswordDto changePasswordDto);

        //Somente em Serviços
        Task<UserDto> GetById(string id);
        Task<bool> SetSellerRole(string idUser);
        

    }
}
