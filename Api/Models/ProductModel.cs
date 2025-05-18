using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class ProductModel : BaseEntity
    {
        public string name { get; set; }
        public float price { get; set; }
        public int categoriesId { get; set; }
        public virtual CategoriesModel categories { get; set; }
        public int shopId { get; set; }
        public virtual ShopModel shop { get; set; }
        public string description { get; set; }

        public IEnumerable<StockModel> stocks { get; set; }
        public IEnumerable<DiscountModel> discounts { get; set; }
        public IEnumerable<ScoreProductModel> scoreProducts { get; set; }
        public IEnumerable<EvidenceModel> evidences { get; set; }
    }
}
