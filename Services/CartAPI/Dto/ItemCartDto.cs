namespace CartAPI.Dto
{
    public class ItemCartDto
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        
    }
}
