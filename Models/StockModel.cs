namespace ApiEstoque.Models
{
    public class StockModel
    {
        public int id { get; set; }
        public float amount { get; set; }
        public string status { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
