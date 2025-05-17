using ApiEstoque.Dto.Adress;

namespace ApiEstoque.Dto.User
{
    public class UserFullDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string typeAccount { get; set; }

        public AddressDto address { get; set; }
    }
}
