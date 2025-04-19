namespace ApiEstoque.Models
{
    public class ImageModel
    {
        public int id { get; set; }
        public string url { get; set; }
        public float size { get; set; }
        public int shopId { get; set; }
        public int productId { get; set; }
        public virtual ShopModel Shop { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
