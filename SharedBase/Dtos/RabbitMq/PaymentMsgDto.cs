using System.Text.Json.Serialization;
using SharedBase.Models;

namespace SharedBase.Dtos.Cart
{
    public class PaymentMsgDto : BaseMessage
    {
        public string UserId { get; set; }
        public bool PaymentStatus { get; set; }
        public float FinalPrice { get; set; }
        public float TotalDiscount { get; set; }
        public int ListCount { get; set; }
        public IEnumerable<PaymentItemDto> ListItem { get; set; }

    }
    public class PaymentItemDto
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceFinal { get; set; }
    }
}
