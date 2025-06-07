namespace ApiEstoque.Dto.ScoreProduct
{
    public class ScoreProductDto
    {
        public Guid id { get; set; }
        public float amountStars { get; set; }
        public Guid productId { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
