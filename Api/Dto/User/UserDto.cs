using ApiEstoque.Dto.Adress;

namespace ApiEstoque.Dto.User
{
    public class UserDto
    {
        public string id { get; set; }
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public string status { get; set; }

        public AddressDto address { get; set; }

    }
}
