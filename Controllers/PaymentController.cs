using ApiEstoque.Dto.PaymentRequest;
using ApiEstoque.Dto.User;
using ApiEstoque.Services.Interface;
using MercadoPago;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using MercadoPago.Resource.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserService _userService;

        public PaymentController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("MercadoPago")]
        public async Task<IActionResult> MercadoPago([FromBody] PaymentRequestDto request)
        {
            UserFullDto user = await _userService.GetUserFullByIdUser(request.UserId);
             int index = user.name.IndexOf(" ");

             // Separa o primeiro nome e o restante
             string primeiroNome = index >= 0 ? user.name.Substring(0, index) : user.name;
             string sobrenome = index >= 0 ? user.name.Substring(index + 1) : "";

             var external_reference_Controll = Guid.NewGuid().ToString();
             var preference = new PreferenceRequest
             {
                 Items = request.CartList.Select(item => new PreferenceItemRequest
                 {
                     Title = item.Product.Name,
                     Quantity = item.Quantity,
                     UnitPrice = item.FinalPrice
                 }).ToList(),
                 Payer = new PreferencePayerRequest
                 {//Dados do Vendedor
                     Email = user.email,
                     Name = user.name,//Conta Vendedo
                     Identification = new IdentificationRequest
                     {
                         Type = request.TypeDocument!.ToUpper(),
                         Number = request.DocumentNumber
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
                 ExpirationDateFrom = DateTime.Now,
                 ExpirationDateTo = DateTime.Now.AddMinutes(10)
             };

             var client = new PreferenceClient();
             var createdPreference = await client.CreateAsync(preference);

             return Ok(new { apiUrl = createdPreference.SandboxInitPoint });
        }

        
    }

}
