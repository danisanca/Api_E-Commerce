using ApiEstoque.Constants;
using SharedBase.Models;

namespace ApiEstoque.Models
{
    public class ProductModel : BaseEntity
    {
        public string name { get; set; }
        public float price { get; set; }
        public Guid categoriesId { get; set; }
        public Guid shopId { get; set; }
        public string description { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();

        public virtual ShopModel shop { get; set; }
        public virtual CategoriesModel categories { get; set; }
        public IEnumerable<StockModel> stocks { get; set; }
        public IEnumerable<DiscountModel> discounts { get; set; }
    }
}
