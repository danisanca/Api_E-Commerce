namespace ApiEstoque.Dto.Discount
{
    public class DiscountDto
    {
        public Guid id { get; set; }
        public float percentDiscount { get; set; }
        public Guid productId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
