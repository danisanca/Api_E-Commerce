using OrderAPI.Models;

namespace OrderAPI.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
