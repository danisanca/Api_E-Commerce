using ApiEstoque.Constants;
using SharedBase.Models;

namespace ApiEstoque.Models
{
    public class DiscountModel:BaseEntity
    {
        public float percentDiscount { get; set; }
        public Guid productId { get; set; }
        public virtual ProductModel product { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();
    }
}
