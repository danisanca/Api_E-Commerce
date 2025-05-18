using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class StockModel : BaseEntity
    {
        public float amount { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
    }
}
