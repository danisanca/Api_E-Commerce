using ApiEstoque.Data;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueTests.RepositoryTest
{
    public class UserRepositoryTest : DbBaseTest,IClassFixture<DbTest>
    {

        private ServiceProvider _serviceProvide;
        public UserRepositoryTest(DbTest dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD  de Usuario")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var apiContext = _serviceProvide.GetService<ApiContext>())
            {
                UserRepository _userRepository = new UserRepository(apiContext);
                
                #region Entities
                UserModel firstEntity = new UserModel
                {
                    name = Faker.Name.First(),
                    email = Faker.Internet.Email(),
                    username = Faker.Name.First() + Faker.RandomNumber.Next(100),
                    password = "123456",
                    status = StandartStatus.Ativo.ToString(),
                    typeAccount = TypeUserEnum.Standart.ToString(),
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };
                UserModel secondEntity = new UserModel
                {
                    name = Faker.Name.First(),
                    email = Faker.Internet.Email(),
                    username = Faker.Name.First() + Faker.RandomNumber.Next(100),
                    password = "123456",
                    status = StandartStatus.Ativo.ToString(),
                    typeAccount = TypeUserEnum.Standart.ToString(),
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };
                UserModel thirdEntity = new UserModel
                {
                    name = Faker.Name.First(),
                    email = Faker.Internet.Email(),
                    username = Faker.Name.First() + Faker.RandomNumber.Next(100),
                    password = "123456",
                    status = StandartStatus.Desabilitado.ToString(),
                    typeAccount = TypeUserEnum.Standart.ToString(),
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };

                firstEntity.SetPasswordHash();
                secondEntity.SetPasswordHash();
                thirdEntity.SetPasswordHash();
                #endregion

                #region Insert_Teste
                //Criando Primeiro usuario
                var createdFirstUser = await _userRepository.AddUser(firstEntity);

                Assert.NotNull(createdFirstUser);
                Assert.Equal(firstEntity.email, createdFirstUser.email);
                Assert.Equal(firstEntity.name, createdFirstUser.name);
                Assert.Equal(firstEntity.username, createdFirstUser.username);
                Assert.Equal(firstEntity.status, createdFirstUser.status);
                Assert.Equal(firstEntity.typeAccount, createdFirstUser.typeAccount);

                //Criando Segundo usuario
                var createdSecoundUser = await _userRepository.AddUser(secondEntity);

                Assert.NotNull(createdSecoundUser);
                Assert.Equal(secondEntity.email, createdSecoundUser.email);
                Assert.Equal(secondEntity.name, createdSecoundUser.name);
                Assert.Equal(secondEntity.username, createdSecoundUser.username);
                Assert.Equal(secondEntity.status, createdSecoundUser.status);
                Assert.Equal(secondEntity.typeAccount, createdSecoundUser.typeAccount);

                //Criando terceiro usuario
                var createdThirdUser = await _userRepository.AddUser(thirdEntity);

                Assert.NotNull(createdThirdUser);
                Assert.Equal(thirdEntity.email, createdThirdUser.email);
                Assert.Equal(thirdEntity.name, createdThirdUser.name);
                Assert.Equal(thirdEntity.username, createdThirdUser.username);
                Assert.Equal(thirdEntity.status, createdThirdUser.status);
                Assert.Equal(thirdEntity.typeAccount, createdThirdUser.typeAccount);
                #endregion

                #region GetById_Teste
                var findUser = await _userRepository.GetUserById(firstEntity.id);
                Assert.NotNull(findUser);
                Assert.Equal(firstEntity.email, findUser.email);
                Assert.Equal(firstEntity.name, findUser.name);
                Assert.Equal(firstEntity.username, findUser.username);
                Assert.Equal(firstEntity.status, findUser.status);
                Assert.Equal(firstEntity.typeAccount, findUser.typeAccount);
                #endregion

                #region Update_Teste
                firstEntity.name = Faker.Name.First();
                firstEntity.email = Faker.Internet.Email();
                var updatedUser = await _userRepository.UpdateUser(firstEntity);
                Assert.True(updatedUser);
                //Confirmando Atualização
                var findUserUpdated = await _userRepository.GetUserById(firstEntity.id);
                Assert.Equal(firstEntity.email, findUserUpdated.email);
                Assert.Equal(firstEntity.name, findUserUpdated.name);
                #endregion

                #region GetAll_Teste
                var findUsers = await _userRepository.GetAllUsers(FilterGetRoutes.All);
                Assert.NotNull(findUsers);
                Assert.True(findUsers.Count() == 3);
                #endregion

                #region GetAllActives_Teste
                var findActivesUsers = await _userRepository.GetAllUsers(FilterGetRoutes.Ativo);
                Assert.NotNull(findActivesUsers);
                Assert.True(findActivesUsers.Count() == 2);
                Assert.All(findActivesUsers, user => Assert.Equal(FilterGetRoutes.Ativo.ToString(), user.status));
                #endregion

                #region GetAllActives_Teste
                var findDisableUsers = await _userRepository.GetAllUsers(FilterGetRoutes.Desabilitado);
                Assert.NotNull(findDisableUsers);
                Assert.True(findDisableUsers.Count() == 1);
                Assert.All(findDisableUsers, user => Assert.Equal(FilterGetRoutes.Desabilitado.ToString(), user.status));
                #endregion

                #region GetByEmail_Teste
                var findUserByEmail = await _userRepository.GetUserByEmail(firstEntity.email);
                Assert.NotNull(findUser);
                Assert.Equal(firstEntity.email, findUserByEmail.email);
                Assert.Equal(firstEntity.name, findUserByEmail.name);
                Assert.Equal(firstEntity.username, findUserByEmail.username);
                Assert.Equal(firstEntity.status, findUserByEmail.status);
                Assert.Equal(firstEntity.typeAccount, findUserByEmail.typeAccount);
                #endregion

                #region GetByUsername_Teste
                var findUserByUsername = await _userRepository.GetUserByUsername(firstEntity.username);
                Assert.NotNull(findUserByUsername);
                Assert.Equal(firstEntity.email, findUserByUsername.email);
                Assert.Equal(firstEntity.name, findUserByUsername.name);
                Assert.Equal(firstEntity.username, findUserByUsername.username);
                Assert.Equal(firstEntity.status, findUserByUsername.status);
                Assert.Equal(firstEntity.typeAccount, findUserByUsername.typeAccount);
                #endregion
            }
        }
    }
}
