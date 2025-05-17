namespace ApiEstoque.Dto.ScoreProduct
{
    public class ScoreProductDto
    {
        public int id { get; set; }
        public float amountStars { get; set; }
        public int productId { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
