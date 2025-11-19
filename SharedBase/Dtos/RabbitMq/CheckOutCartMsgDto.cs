using System.Text.Json.Serialization;
using SharedBase.Dtos.Cart;
using SharedBase.Models;

namespace SharedBase.Dtos.RabbitMq
{
    public class CheckOutCartMsgDto : BaseMessage
    {
        
        public string UserId { get; set; } = "";
        public int ListCount { get; set; } = 0;


        public IEnumerable<CartDetailDto>? CartDetail { get; set; } = new List<CartDetailDto>();

    }
}
