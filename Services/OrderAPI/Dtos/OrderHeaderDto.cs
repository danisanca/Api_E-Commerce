using OrderAPI.Models;

namespace OrderAPI.Dtos
{
    public class OrderHeaderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public float TotalPrice { get; set; }
        public float TotalDiscont { get; set; }
        public int ListCount { get; set; }
        public string ExternalReference { get; set; } 
        public string PreferenceId { get; set; }
        public string InitPoint { get; set; }
        public DateTime ReferenceCreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<OrderDetailsDto> OrderDetailsDto { get; set; }
    }
}
