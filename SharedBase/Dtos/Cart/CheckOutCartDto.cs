using System.Text.Json.Serialization;
using SharedBase.Models;

namespace SharedBase.Dtos.Cart
{
    public class CheckOutCartDto : BaseMessage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMothYear { get; set; }

       
        [JsonIgnore]
        public IEnumerable<CartDetailDto>? CartDetail { get; set; } = new List<CartDetailDto>();

    }
}
