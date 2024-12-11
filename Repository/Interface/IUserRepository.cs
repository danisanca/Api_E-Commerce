using ApiEstoque.Dto.User;
using ApiEstoque.Models;
using ApiEstoque.Helpers;

namespace ApiEstoque.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers(FilterGetRoutes status);
        Task<UserModel> GetUserById(int idUser);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByUsername(string username);
        Task<UserModel> AddUser(UserModel userCreate);
        Task<bool> UpdateUser(UserModel userUpdate); 
    }
}
