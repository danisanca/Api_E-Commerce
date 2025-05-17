using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEstoqueTests.ServiceTest
{
    public class UserServiceTest : UserBaseTest
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Given CreateDto Valid When CreateUserService then should UserDto Valid .")]
        public async Task Given_CreateDto_Valid_When_CreateUserService_then_should_UserDto_Valid_()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.CreateUser(userCreateDto,TypeUserEnum.Standart,null)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.CreateUser(userCreateDto, TypeUserEnum.Standart, null);
            Assert.NotNull(result);
            Assert.Equal(nameUserMok, result.name);
            Assert.Equal(emailUserMok, result.email);
            Assert.Equal(usernameUserMok, result.username);
            Assert.Equal(statusActiveUserMok, result.status);
            Assert.Equal(typeAccountUserMok, result.typeAccount);

        }

        [Fact(DisplayName = "Given UpdateDto Valid When UpdateUser then should True")]
        public async Task Given_UpdateDto_Valid_When_UpdateUser_then_should_True()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.CreateUser(userCreateDto, TypeUserEnum.Standart, null)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.CreateUser(userCreateDto, TypeUserEnum.Standart, null);
            Assert.NotNull(result);
            Assert.Equal(nameUserMok, result.name);
            Assert.Equal(emailUserMok, result.email);
            Assert.Equal(usernameUserMok, result.username);
            Assert.Equal(statusActiveUserMok, result.status);
            Assert.Equal(typeAccountUserMok, result.typeAccount);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.UpdateUser(userUpdateDto)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.UpdateUser(userUpdateDto);
            Assert.True(resultUpdate);
        }

        [Fact(DisplayName = "Given idUser Valid When ActiveUser then should True")]
        public async Task Given_idUser_Valid_When_ActiveUser_then_should_True()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.ActiveUser(IdUserMok)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var resultActiveUser = await _service.ActiveUser(IdUserMok);
            Assert.True(resultActiveUser);

        }

        [Fact(DisplayName = "Given idUser Valid When DisableUser then should True")]
        public async Task Given_idUser_Valid_When_DisableUser_then_should_True()
        {

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.DisableUser(IdUserMok)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var resultDisableUser = await _service.DisableUser(IdUserMok);
            Assert.True(resultDisableUser);
        }

        [Fact(DisplayName = "Given idUser Valid When GetUserById then should True")]
        public async Task Given_idUser_Valid_When_GetUserById_then_should_True()
        {
            //Validando retorno quando id é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserById(IdUserMok)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.GetUserById(IdUserMok);
            Assert.NotNull(result);
            Assert.True(result.id == IdUserMok);
            Assert.Equal(emailUserMok, result.email);

            //Validando retorno quando id nao é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(Task.FromResult((UserDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetUserById(IdUserMok);
            Assert.Null(_record);
        }

        [Fact(DisplayName = "Given email Valid When GetUserByEmail then should True")]
        public async Task Given_email_Valid_When_GetUserByEmail_then_should_True()
        {
            //Validando retorno quando email é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserByEmail(emailUserMok)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.GetUserByEmail(emailUserMok);
            Assert.NotNull(result);
            Assert.True(result.email == emailUserMok);

            //Validando retorno quando email nao é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult((UserDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetUserByEmail(emailUserMok);
            Assert.Null(_record);
        }
        
        [Fact(DisplayName = "Given username Valid When GetUserByUsername then should True")]
        public async Task Given_username_Valid_When_GetUserByUsername_then_should_True()
        {
            //Validando retorno quando username é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserByUsername(usernameUserMok)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.GetUserByUsername(usernameUserMok);
            Assert.NotNull(result);
            Assert.True(result.username == usernameUserMok);

            //Validando retorno quando username nao é localizado
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult((UserDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.GetUserByUsername(usernameUserMok);
            Assert.Null(_record);
        }

        [Fact(DisplayName = "Given empty Params When getAllUsers then should True")]
        public async Task Given_empty_params_When_GetAllUsers_then_should_True()
        {
            //Validando retorno quando a lista é diferente de vazia
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAllUsers(FilterGetRoutes.All)).ReturnsAsync(listaUserDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAllUsers(FilterGetRoutes.All);
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);

            // Validando retorno quando a lista é vazia
            var listResult = new List<UserDto>();
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAllUsers(FilterGetRoutes.All)).ReturnsAsync(listResult);
            _service = _serviceMock.Object;

            var resultEmpty = await _service.GetAllUsers(FilterGetRoutes.All);
            Assert.Empty(resultEmpty);
            Assert.True(resultEmpty.Count() == 0);

        }
    }
}
