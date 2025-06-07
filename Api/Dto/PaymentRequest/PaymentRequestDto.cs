
namespace ApiEstoque.Dto.PaymentRequest
{
    public class PaymentRequestDto
    {
        public string UserId { get; set; }
        public List<CartItemDTO> CartList { get; set; }
        public float finalPrice { get; set; }
        public string TypePayment { get; set; } // "credit_card", "pix", "boleto"
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TypeDocument { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
    }
  

}
