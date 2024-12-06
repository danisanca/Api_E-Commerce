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

        public async Task AddUser(UserModel userCreate)
        {
            await _db.User.AddAsync(userCreate);
            await _db.SaveChangesAsync();
        }


        public async Task<List<UserModel>> GetAllUsers(FilterGetRoutes status)
        {
            if (status == FilterGetRoutes.Ativo) return await _db.User.Where(g => g.status == status.ToString()).ToListAsync();
            else if (status == FilterGetRoutes.Desabilitado) return await _db.User.Where(g => g.status == status.ToString()).ToListAsync();
            else return await _db.User.ToListAsync();
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<UserModel> GetUserById(int idUser)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.id == idUser);
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.username == username);
        }

        public async Task UpdateUser(UserModel userUpdate)
        {
            _db.User.Update(userUpdate);
            await _db.SaveChangesAsync();
        }
    }
}
