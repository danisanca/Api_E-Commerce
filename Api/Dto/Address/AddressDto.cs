namespace ApiEstoque.Dto.Adress
{
    public class AddressDto
    {
        public int id { get; set; }
        public string street { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string cellPhone { get; set; }
        public int userId { get; set; }
    }
}
