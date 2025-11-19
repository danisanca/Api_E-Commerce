namespace SharedBase.Dtos.Cart
{
    public class CartDetailDto
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public int Count { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

    }
}
