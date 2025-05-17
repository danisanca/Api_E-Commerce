using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueTests.ServiceTest
{
    public class UserBaseTest
    {
        public static int IdUserMok { get; set; }
        public static String nameUserMok { get; set; }
        public static String usernameUserMok { get; set; }
        public static String emailUserMok { get; set; }
        public static String passwordUserMok { get; set; }
        public static String statusActiveUserMok { get; set; }
        public static String typeAccountUserMok { get; set; }
        public static String statusDisableUserMok { get; set; }

        public List<UserDto> listaUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserDto disableUserDto;
        public UserCreateDto userCreateDto;
        public UserUpdateDto userUpdateDto;

        public UserBaseTest()
        {
            IdUserMok = Faker.RandomNumber.Next(50);
            nameUserMok = Faker.Name.FullName();
            usernameUserMok = Faker.Name.First();
            passwordUserMok = "123456";
            emailUserMok = Faker.Internet.Email();
            statusActiveUserMok = StandartStatus.Ativo.ToString();
            typeAccountUserMok = TypeUserEnum.Standart.ToString();
            statusDisableUserMok = StandartStatus.Desabilitado.ToString();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    id = IdUserMok,
                    name = nameUserMok,
                    email = emailUserMok,
                    username = usernameUserMok,
                    status = statusActiveUserMok,
                    typeAccount = typeAccountUserMok
                };
                listaUserDto.Add(dto);
            }

            userDto = new UserDto
            {
                id = IdUserMok,
                name = nameUserMok,
                email = emailUserMok,
                username = usernameUserMok,
                status = statusActiveUserMok,
                typeAccount = typeAccountUserMok
            };

            disableUserDto = new UserDto
            {
                id = IdUserMok,
                name = nameUserMok,
                email = emailUserMok,
                username = usernameUserMok,
                status = statusDisableUserMok,
                typeAccount = typeAccountUserMok
            };

            userCreateDto = new UserCreateDto
            {
                name = nameUserMok,
                email = emailUserMok,
                username = usernameUserMok,
                password = passwordUserMok
            };
            userUpdateDto = new UserUpdateDto
            {
                id = IdUserMok,
                name = nameUserMok,
                email = emailUserMok
            };

        }

    }
}
