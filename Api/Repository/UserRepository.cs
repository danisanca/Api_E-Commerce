using ApiEstoque.Data;
using ApiEstoque.Dto.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiEstoque.Helpers;

namespace ApiEstoque.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiContext _db;

        public UserRepository(ApiContext db)
        {
            _db = db;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.username == username);
        }
    }
}
