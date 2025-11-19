using OrderAPI.Dtos;

namespace OrderAPI.Services.Interface
{
    public interface IPaymentService
    {
        Task<MercadoPagoResult> PaymentMercadoPago(PaymentRequestDto paymentRequestDto);
    }
}
