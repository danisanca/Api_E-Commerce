using OrderAPI.Models;

namespace OrderAPI.Dtos
{
    public class OrderHeaderDto
    {
        public Guid Id { get; set; }
        public float TotalPrice { get; set; }
        public float TotalDiscont { get; set; }
        public int ListCount { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<OrderDetailsDto> OrderDetailsDto { get; set; }
    }
}
