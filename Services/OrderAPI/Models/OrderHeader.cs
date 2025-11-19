using SharedBase.Models;

namespace OrderAPI.Models
{
    public class OrderHeader : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public decimal TotalDiscont { get; set; } = 0;
        public int ListCount { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
        public string PreferenceId { get; set; } = string.Empty;
        public string InitPoint { get; set; } = string.Empty;
        public DateTime ReferenceCreatedAt { get; set; }
        public DateTime ExpireAt {  get; set; }
        public bool PaymentStatus { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
