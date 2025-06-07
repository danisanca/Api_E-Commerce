using ApiEstoque.Models;
using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class StockModel : BaseEntity
    {
        public float amount { get; set; }
        public Guid productId { get; set; }
        public virtual ProductModel product { get; set; }
    }
}
