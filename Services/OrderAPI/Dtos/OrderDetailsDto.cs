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
        public float Price { get; set; }
    }
}
