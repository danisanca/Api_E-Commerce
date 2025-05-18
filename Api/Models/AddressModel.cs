using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class AddressModel:BaseEntity
    {
        public string street { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string cellPhone { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
    }
}
