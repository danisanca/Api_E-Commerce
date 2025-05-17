using ApiEstoque.Controllers;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel;
using Xunit;

namespace ApiEstoqueTests.ControllerTest
{
    public class UserControllerTest
    {
        private UserController userController;

        // - ROTAS POST
        [Fact(DisplayName = "Given valid data, when create standart user then should Success.")]
        public async Task GivenValidData_WhenCreateStandartUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Standart";

            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Standart, null)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    name = name,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                name = name,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserStandart(user);


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult.Value);
            Assert.Equal(typeAccount, ((UserDto)objectResult.Value).typeAccount);
        }

        [Fact(DisplayName = "Given invalid data, when create standart user then should BadRequest.")]
        public async Task GivenInvalidData_WhenCreateStandartUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Standart";


            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Standart, null)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Nome", "Nome é um campo obrigatório.");

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserStandart(user);

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            //Verifica se o name é null
            Assert.True(user.name is null);
            //Verifica se o service foi chamado
            serviceMock.Verify(m => m.CreateUser(It.IsAny<UserCreateDto>(), It.IsAny<TypeUserEnum>(), null), Times.Never);
        }

        [Fact(DisplayName = "Given valid data, when create owner user then should Success.")]
        public async Task GivenValidData_WhenCreateOwnerUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Owner";

            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Owner, null)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    name = name,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                name = name,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserOwner(user);


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult.Value);
            Assert.Equal(typeAccount, ((UserDto)objectResult.Value).typeAccount);
        }

        [Fact(DisplayName = "Given invalid data, when create owner user then should BadRequest.")]
        public async Task GivenInvalidData_WhenCreateOwnerUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Standart";


            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Owner, null)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Nome", "Nome é um campo obrigatório.");

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserOwner(user);

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            //Verifica se o name é null
            Assert.True(user.name is null);
            //Verifica se o service foi chamado
            serviceMock.Verify(m => m.CreateUser(It.IsAny<UserCreateDto>(), It.IsAny<TypeUserEnum>(), null), Times.Never);
        }

        [Fact(DisplayName = "Given valid data, when create admin user then should Success.")]
        public async Task GivenValidData_WhenCreateAdminUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Admin";
            var tokenAdmin = "123456";

            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Admin, tokenAdmin)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    name = name,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                name = name,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserAdmin(user, tokenAdmin);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult.Value);
            Assert.Equal(typeAccount, ((UserDto)objectResult.Value).typeAccount);
        }

        [Fact(DisplayName = "Given invalid data, when create owner user then should BadRequest.")]
        public async Task GivenInvalidData_WhenCreateAdminUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var password = "123456";
            var status = "Ativo";
            var typeAccount = "Admin";
            var tokenAdmin = "123456";


            serviceMock.Setup(m => m.CreateUser(It.IsAny<UserCreateDto>(), TypeUserEnum.Admin, null)).ReturnsAsync(
                new UserDto
                {
                    id = 1,
                    email = email,
                    username = userName,
                    status = status,
                    typeAccount = typeAccount

                }
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Nome", "Nome é um campo obrigatório.");

            UserCreateDto user = new UserCreateDto
            {
                email = email,
                username = userName,
                password = password
            };

            var result = await userController.CreateUserAdmin(user, tokenAdmin);

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            //Verifica se o name é null
            Assert.True(user.name is null);
            //Verifica se o service foi chamado
            serviceMock.Verify(m => m.CreateUser(It.IsAny<UserCreateDto>(), It.IsAny<TypeUserEnum>(), null), Times.Never);
        }

        // -ROTAS GET
        [Fact(DisplayName = "Given Valid idUser, when GetUserById then should Success.")]
        public async Task GivenIdUser_WhenGetUserById_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();
            var id = 1;
            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var status = "Ativo";
            var typeAccount = "Owner";

            serviceMock.Setup(m => m.GetUserById(It.IsAny<int>())).ReturnsAsync(
               new UserDto
               {
                   id = 1,
                   email = email,
                   name = name,
                   username = userName,
                   status = status,
                   typeAccount = typeAccount
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetUserById(id);


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult?.Value);
            Assert.Equal(id, ((UserDto)objectResult.Value).id);
        }

        [Fact(DisplayName = "Given Wrong idUser, when GetUserById then should BadRequest.")]
        public async Task GivenWrongIdUser_WhenGetUserById_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetUserById(It.IsAny<int>())).ReturnsAsync((UserDto)null);


            userController = new UserController(serviceMock.Object);

            userController.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await userController.GetUserById(0);

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            serviceMock.Verify(m => m.GetUserById(It.IsAny<int>()), Times.Never);
        }

        [Fact(DisplayName = "Given Valid Username, when GetUserByUsername then should Success.")]
        public async Task GivenUsername_WhenGetUserByUsername_ThennShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();
            var id = 1;
            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var status = "Ativo";
            var typeAccount = "Owner";

            serviceMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(
               new UserDto
               {
                   id = 1,
                   email = email,
                   name = name,
                   username = userName,
                   status = status,
                   typeAccount = typeAccount
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetUserByUsername(userName);


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult?.Value);
            Assert.Equal(userName, ((UserDto)objectResult.Value).username);
        }

        [Fact(DisplayName = "Given Wrong Username, when GetUserByUsername then should BadRequest.")]
        public async Task GivenWrongUsername_WhenGetUserByUsername_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).ReturnsAsync((UserDto)null);


            userController = new UserController(serviceMock.Object);

            userController.ModelState.AddModelError("Username", "Formato Inválido");

            var result = await userController.GetUserByUsername("");

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            serviceMock.Verify(m => m.GetUserByUsername(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Given Valid Email, when GetUserByEmail then should Success.")]
        public async Task GivenEmail_WhenGetUserByEmail_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();
            var id = 1;
            var name = Faker.Name.First();
            var email = Faker.Internet.Email();
            var userName = name + Faker.RandomNumber.Next(5000);
            var status = "Ativo";
            var typeAccount = "Owner";

            serviceMock.Setup(m => m.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(
               new UserDto
               {
                   id = 1,
                   email = email,
                   name = name,
                   username = userName,
                   status = status,
                   typeAccount = typeAccount
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetUserByEmail(email);


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult?.Value);
            Assert.Equal(email, ((UserDto)objectResult.Value).email);
        }

        [Fact(DisplayName = "Given Wrong email, when GetUserByEmail then should BadRequest.")]
        public async Task GivenWrongEmail_WhenGetUserByEmail_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((UserDto)null);


            userController = new UserController(serviceMock.Object);

            userController.ModelState.AddModelError("email", "Formato Inválido");

            var result = await userController.GetUserByEmail("");

            //Verifica se o retorno é um BadRequest
            Assert.True(result is BadRequestObjectResult);
            serviceMock.Verify(m => m.GetUserByEmail(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Given empty Params, when GetAllUser then should Success.")]
        public async Task GivenEmptyParams_WhenGetAllUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetAllUsers(FilterGetRoutes.All)).ReturnsAsync(
               new List<UserDto>
               {
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Ativo",
                   typeAccount = "Owner"
                  },
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Desabilitado",
                   typeAccount = "Owner"
                  },
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Bloqueado",
                   typeAccount = "Owner"
                  }
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetAllUsers();


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Count() == 3);
        }

        [Fact(DisplayName = "Given empty Params, when GetAllActivesUser then should Success.")]
        public async Task GivenEmptyParams_WhenGetAllActivesUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetAllUsers(FilterGetRoutes.Ativo)).ReturnsAsync(
               new List<UserDto>
               {
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Ativo",
                   typeAccount = "Owner"
                  },
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Ativo",
                   typeAccount = "Owner"
                  }
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetAllUsersActives();


            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Count() == 2);
            Assert.All(resultValue, user => Assert.Equal("Ativo", user.status));
        }

        [Fact(DisplayName = "Given empty Params, when GetAllDesactiveUser then should Success.")]
        public async Task GivenEmptyParams_WhenGetAllDesactiveUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();


            serviceMock.Setup(m => m.GetAllUsers(FilterGetRoutes.Desabilitado)).ReturnsAsync(
               new List<UserDto>
               {
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Desabilitado",
                   typeAccount = "Owner"
                  },
                  new UserDto
                  {
                   id = Faker.RandomNumber.Next(2),
                   email = Faker.Internet.Email(),
                   name = Faker.Name.First(),
                   username = Faker.Name.First() + Faker.RandomNumber.Next(5000),
                   status = "Desabilitado",
                   typeAccount = "Owner"
                  }
               }
           );
            userController = new UserController(serviceMock.Object);

            var result = await userController.GetAllUsersDesactive();

            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkObjectResult>(result);

            var resultValue = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Count() == 2);
            Assert.All(resultValue, user => Assert.Equal("Desabilitado", user.status));
        }

        // -ROTAS PUT
        [Fact(DisplayName = "Given valid data, when UpdateUser then should Success.")]
        public async Task GivenValidData_WhenUpdateUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var id = 1;
            var newName = Faker.Name.First();
            var NewEmail = Faker.Internet.Email();
            var userName = Faker.Name.First() + Faker.RandomNumber.Next(5000);

            serviceMock.Setup(m => m.UpdateUser(It.IsAny<UserUpdateDto>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);

            UserUpdateDto user = new UserUpdateDto
            {
                id = id,
                email = NewEmail,
                name = newName
            };

            var result = await userController.UpdateUserById(user);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "Given invalid data, when UpdateUser then should BadRequest.")]
        public async Task GivenInvalidData_WhenUpdateUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            var id = 1;
            var newName = Faker.Name.First();
            var NewEmail = Faker.Internet.Email();
            var userName = Faker.Name.First() + Faker.RandomNumber.Next(5000);

            serviceMock.Setup(m => m.UpdateUser(It.IsAny<UserUpdateDto>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Email", "É um campo obrigatório");
            UserUpdateDto user = new UserUpdateDto
            {
                id = id,
                email = NewEmail,
                name = newName
            };

            var result = await userController.UpdateUserById(user);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<BadRequestObjectResult>(result);
            serviceMock.Verify(m => m.UpdateUser(It.IsAny<UserUpdateDto>()), Times.Never);
        }

        [Fact(DisplayName = "Given valid data, when ActiveUser then should Success.")]
        public async Task GivenValidData_WhenActiveUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var id = 1;

            serviceMock.Setup(m => m.ActiveUser(It.IsAny<int>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);


            var result = await userController.ActiveUserById(id);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "Given invalid data, when ActiveUser then should BadRequest.")]
        public async Task GivenInvalidData_WhenActiveUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.ActiveUser(It.IsAny<int>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Id", "É um campo obrigatório");

            var result = await userController.ActiveUserById(0);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<BadRequestObjectResult>(result);
            serviceMock.Verify(m => m.ActiveUser(It.IsAny<int>()), Times.Never);

        }

        [Fact(DisplayName = "Given valid data, when DisableUser then should Success.")]
        public async Task GivenValidData_WhenDisableUser_ThenShouldSuccess()
        {
            var serviceMock = new Mock<IUserService>();

            var id = 1;

            serviceMock.Setup(m => m.DisableUser(It.IsAny<int>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);


            var result = await userController.DisableUserById(id);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<OkResult>(result);

        }

        [Fact(DisplayName = "Given invalid data, when DisableUser then should BadRequest.")]
        public async Task GivenInvalidData_WhenDisableUser_ThenShouldBadRequest()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.DisableUser(It.IsAny<int>())).ReturnsAsync(
                true
            );

            userController = new UserController(serviceMock.Object);
            userController.ModelState.AddModelError("Id", "É um campo obrigatório");

            var result = await userController.DisableUserById(0);

            //Verifica se o retorno é um 200 Http
            Assert.IsType<BadRequestObjectResult>(result);
            serviceMock.Verify(m => m.ActiveUser(It.IsAny<int>()), Times.Never);

        }
    }
}