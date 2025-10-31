
using Azure.Core;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using OrderAPI.Dtos;
using OrderAPI.Services.Interface;

namespace OrderAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderServices _orderServices;
        public PaymentService(IOrderServices orderServices) {
            _orderServices = orderServices;
        }
        public async Task<MercadoPagoResult> PaymentMercadoPago(PaymentRequestDto paymentRequestDto)
        {
            var external_reference_Controll = Guid.NewGuid().ToString();
            var preference = new PreferenceRequest
            {
                Items = paymentRequestDto.Itens.Select(item => new PreferenceItemRequest
                {
                    Title = item.ProductName,
                    Quantity = item.Count,
                    UnitPrice = item.Price
                }).ToList(),
                Payer = new PreferencePayerRequest
                {//Dados do Vendedor
                    Email = "vendedor@example.com",
                    Name = "FirsNameVendedor",//Conta Vendedo
                    Identification = new IdentificationRequest
                    {
                        Type = paymentRequestDto.TypeDocument!.ToUpper(),
                        Number = paymentRequestDto.DocumentNumber
                    }
                },
                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    ExcludedPaymentMethods = [],
                    ExcludedPaymentTypes = [],
                    Installments = 8
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "http://localhost:4200/finishPayment",
                    Failure = "http://localhost:4200/cart",
                    Pending = "http://localhost:4200/perfil/historyPurchase"
                },
                AutoReturn = "approved",
                StatementDescriptor = "SiteE-Commerce",
                ExternalReference = $"Referencia_{external_reference_Controll}",
                Expires = true,
                ExpirationDateFrom = DateTime.UtcNow,
                ExpirationDateTo = DateTime.UtcNow.AddMinutes(10)
            };
            var client = new PreferenceClient();
            var createdPreference = await client.CreateAsync(preference);

            var orderHeader = await _orderServices.GetHeaderById(paymentRequestDto.OrderHeaderId);
            orderHeader.ExternalReference = external_reference_Controll;
            orderHeader.PreferenceId = createdPreference.Id;
            orderHeader.InitPoint = createdPreference.SandboxInitPoint;
            orderHeader.ReferenceCreatedAt = DateTime.UtcNow;
            orderHeader.ExpireAt = DateTime.UtcNow.AddMinutes(10);

            await _orderServices.UpdateHeader(orderHeader);

            var result = new MercadoPagoResult (){ apiUrl = createdPreference.SandboxInitPoint };

            return result;
        }
    }
}
