using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using ApiEstoque.Constants;
using Microsoft.AspNetCore.Identity;
using static ApiEstoque.Constants.Roles;
using System.Security.Claims;
using ApiEstoque.Repository.Base;

namespace ApiEstoque.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressService _addressService;
        public UserService(
            UserManager<UserModel> userManager,
            RoleManager<IdentityRole> roleManager,
            IAddressService addressService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _addressService = addressService;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(changePasswordDto.userId);
                if (user == null)
                    return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

                return await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }

        public async Task<bool> Create(UserCreateDto userCreateDto)
        {
            try
            {
                var identityUser = new UserModel
                {
                    UserName = userCreateDto.UserName,
                    Email = userCreateDto.Email,
                    FirstName = userCreateDto.FirstName,
                    LastName = userCreateDto.LastName,
                };

                var result = await _userManager.CreateAsync(identityUser, userCreateDto.Password);
                if (result.Succeeded)
                {
                    //Verifica se existe uma role User, caso nao exita ele cria a role para atribuir ao usuario
                    if (!_roleManager.RoleExistsAsync(RoleHelper.GetRoleName(TypeUserRole.User)).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(RoleHelper.GetRoleName(TypeUserRole.User)));
                    }
                    if (!_roleManager.RoleExistsAsync(RoleHelper.GetRoleName(AccessRole.Standard)).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(RoleHelper.GetRoleName(AccessRole.Standard)));
                    }
                    //Vinculo a Role ao id do usuario.
                    await _userManager.AddToRoleAsync(identityUser, RoleHelper.GetRoleName(TypeUserRole.User));
                    await _userManager.AddToRoleAsync(identityUser, RoleHelper.GetRoleName(AccessRole.Standard));

                    //Cria a claim e vincula ao usuario
                    await _userManager.AddClaimsAsync(identityUser, new Claim[]
                    {
                        new Claim(ClaimTypeCustom.Email, identityUser.Email),
                        new Claim(ClaimTypeCustom.PreferredUserName, identityUser.UserName)
                    });
                    //Cria o endereço
                    var model = userCreateDto.AddressCreateDto;
                    model.userId = identityUser.Id;
                    await _addressService.Create(model);
                
                }

                return result.Succeeded;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDto> GetById(string id)
        {
            try
            { 
                var findUser = await _userManager.FindByIdAsync(id);
                if (findUser == null) throw new FailureRequestException(404, "Não existe usuario com esse id");
                var addressDto = await _addressService.GetByUserId(findUser.Id);
                return new UserDto
                {
                    id = id,
                    nomeCompleto=$"{findUser.FirstName} {findUser.LastName}",
                    email = findUser.Email,
                    status = findUser.Status,
                    address = addressDto
                };

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SetSellerRole(string idUser)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(idUser);
                if (!_roleManager.RoleExistsAsync(RoleHelper.GetRoleName(AccessRole.Seller)).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleHelper.GetRoleName(AccessRole.Seller)));
                }
                //Vinculo a Role ao id do usuario.
                await _userManager.AddToRoleAsync(findUser, RoleHelper.GetRoleName(AccessRole.Seller));
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Update(UserUpdateDto userUpdateDto)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(userUpdateDto.id);
                if (findUser == null) throw new FailureRequestException(404, "Não existe usuario com esse id");
                findUser.Email = userUpdateDto.email;
                findUser.UpdatedAt = DateTime.Now;
                await _userManager.UpdateAsync(findUser);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
