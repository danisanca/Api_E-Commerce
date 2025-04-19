namespace ApiEstoque.Dto.Discount
{
    public class DiscountDto
    {
        public int id { get; set; }
        public string description { get; set; }
        public float percentDiscount { get; set; }
        public int productId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
