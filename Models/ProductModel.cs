namespace ApiEstoque.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int categoriesId { get; set; }
        public virtual CategoriesModel categories { get; set; }
        public int? imageId { get; set; }
        public virtual ImageModel? image { get; set; }
        public int shopId { get; set; }
        public virtual ShopModel shop { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

        public IEnumerable<StockModel> stocks { get; set; }
        public IEnumerable<ScoreProductModel> scoreProducts { get; set; }
        public IEnumerable<EvidenceModel> evidences { get; set; }
    }
}
