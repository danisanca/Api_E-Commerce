using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class OrderHeaderModel : BaseEntity
    {

        public string userId { get; set; }
        public Guid addressId { get; set; }
        public int quantityItem { get; set; }
        public float finalPrice { get; set; }
        public string documentTpe { get; set; }
        public string document {  get; set; }
        public string? preferenceID { get; set; }
        public string? InitPoint { get; set; }

        public virtual UserModel user { get; set; }
        public virtual AddressModel address { get; set; }
        public virtual IEnumerable<OrderDetailModel> orderDetails { get; set; }
    }
}
