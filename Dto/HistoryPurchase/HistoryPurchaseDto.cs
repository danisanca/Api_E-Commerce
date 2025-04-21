using ApiEstoque.Dto.PaymentRequest;

namespace ApiEstoque.Dto.HistoryPurchase
{
    public class HistoryPurchaseDto
    {
        public int id { get; set; }
        public List<CartItemDTO> cartProducts { get; set; }
        public float price { get; set; }
        public int userId { get; set; }
        public string externalReference { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
