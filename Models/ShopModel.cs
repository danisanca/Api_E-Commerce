namespace ApiEstoque.Models
{
    public class ShopModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

        public IEnumerable<CategoriesModel> categories { get; set; }
        public IEnumerable<ProductModel> products { get; set; }

    }
}
