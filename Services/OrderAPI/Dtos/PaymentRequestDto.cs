using OrderAPI.Models;
using SharedBase.Constants;

namespace OrderAPI.Dtos
{
    public class PaymentRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public PaymentMethods TypePayment { get; set; } 
        public CreditCardDto CreditCard { get; set; } = new CreditCardDto();
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TypeDocument { get; set; }
        public string DocumentNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid OrderHeaderId { get; set; }
        public List<OrderDetail> Itens { get; set; }
    }
}
