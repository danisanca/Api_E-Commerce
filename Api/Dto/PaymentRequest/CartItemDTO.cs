using ApiEstoque.Dto.Product;

namespace ApiEstoque.Dto.PaymentRequest
{
    public class CartItemDTO
    {
        public Guid IdProduct { get; set; }
        public ProductDetailsDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
