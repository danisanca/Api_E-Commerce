using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class DiscountModel:BaseEntity
    {
        public float percentDiscount { get; set; }
        public string description { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
    }
}
