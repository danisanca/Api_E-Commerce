using ApiEstoque.Dto.User;
using ApiEstoque.Models;
using ApiEstoque.Helpers;

namespace ApiEstoque.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByUsername(string username);
    }
}
