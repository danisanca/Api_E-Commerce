namespace ApiEstoque.Models
{
    public class ScoreProductModel
    {
        public int id { get; set; }
        public float amountStars { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
        
        
    }
}
