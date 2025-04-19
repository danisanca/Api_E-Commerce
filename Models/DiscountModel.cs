namespace ApiEstoque.Models
{
    public class DiscountModel
    {
        public int id { get; set; }
        public float percentDiscount { get; set; }
        public string description { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
