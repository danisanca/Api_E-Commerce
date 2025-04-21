using ApiEstoque.Dto.Product;

namespace ApiEstoque.Dto.PaymentRequest
{
    public class CartItemDTO
    {
        public int IdProduct { get; set; }
        public ProductFullDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
