using ApiEstoque.Constants;
using SharedBase.Models;
using ApiEstoque.Models;

namespace ApiEstoque.Models
{
    public class StockModel : BaseEntity
    {
        public float amount { get; set; }
        public Guid productId { get; set; }
        public Guid shopId { get; set; }
        public virtual ProductModel product { get; set; }
        public virtual ShopModel shop { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();
    }
}
