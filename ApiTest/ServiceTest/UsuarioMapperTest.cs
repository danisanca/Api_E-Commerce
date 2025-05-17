using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueTests.ServiceTest
{
    public class UsuarioMapperTest : BaseServiceTest
    {
        [Fact(DisplayName = "É Possivel Mappear os Modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                name = Faker.Name.FullName(),
                email = Faker.Internet.Email(),
                username = Faker.Name.First(),
                status = StandartStatus.Ativo.ToString(),
                typeAccount = TypeUserEnum.Standart.ToString()
            };
            var userCreateDto = new UserCreateDto
            {
                name = Faker.Name.FullName(),
                email = Faker.Internet.Email(),
                username = Faker.Name.First(),
                password = "123456",
            };
            var updateUserDto = new UserUpdateDto
            {
                name = Faker.Name.FullName(),
                email = Faker.Internet.Email(),
            };


            //Dto => Model
            var usetDto = Mapper.Map<UserDto>(model);
            Assert.Equal(usetDto.id, model.id);
            Assert.Equal(usetDto.name, model.name);
            Assert.Equal(usetDto.email, model.email);
            Assert.Equal(usetDto.username, model.username);

            var userModelToCreate = Mapper.Map<UserModel>(userCreateDto);
            Assert.Equal(userModelToCreate.name, userCreateDto.name);
            Assert.Equal(userModelToCreate.email, userCreateDto.email);
            Assert.Equal(userModelToCreate.username, userCreateDto.username);
            Assert.Equal(userModelToCreate.password, userCreateDto.password);

            var userModelToUpdate = Mapper.Map<UserModel>(updateUserDto);
            Assert.Equal(userModelToUpdate.name, updateUserDto.name);
            Assert.Equal(userModelToUpdate.email, updateUserDto.email);

        }
    }
}
