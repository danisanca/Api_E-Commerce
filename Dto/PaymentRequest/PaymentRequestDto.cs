using ApiEstoque.Dto.Product;

namespace ApiEstoque.Dto.PaymentRequest
{
    public class PaymentRequestDto
    {
        public int UserId { get; set; }
        public List<CartItemDTO> CartList { get; set; }
        public string TypePayment { get; set; } // "credit_card", "pix", "boleto"
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TypeDocument { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
    }
    public class CartItemDTO
    {
        public int IdProduct { get; set; }
        public ProductFullDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
    

}
