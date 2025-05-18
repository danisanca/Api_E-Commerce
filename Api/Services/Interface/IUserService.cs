using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Helpers;

namespace ApiEstoque.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers(FilterGetRoutes status = FilterGetRoutes.Ativo);
        Task<UserDto> GetUserById(int idUser);
        Task<UserFullDto> GetUserFullByIdUser(int idUser);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByUsername(string username);
        Task<UserDto> CreateUser(UserCreateDto userCreate, TypeUserEnum typeUser, string? TokenAdmin = null);
        Task<bool> UpdateUser(UserUpdateDto userUpdate);
        Task<bool> ActiveUser(int idUser);
        Task<bool> DisableUser(int idUser);
        Task<bool> ChangePassword(ChangePasswordDto modelPassword);
    }
}
