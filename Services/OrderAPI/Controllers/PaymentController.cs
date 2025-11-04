using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Dtos;
using OrderAPI.Services.Interface;
using SharedBase.Dtos.Cart;
using SharedBase.Dtos.RabbitMq;
using SharedBase.Helpers.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [SwaggerOperation(
           Summary = "Pagamento Com Mercado Pago",
           Description = "Rota responsavel por realizar o pagamento do pedido via mercado pago."
         )]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Problemas para processar pagamento.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrilho Localizado.", typeof(MercadoPagoResult))]
        [HttpPost]
        [Route("CheckOut")]
        [HttpPost]
        [Route("PaymentMercadoPago")]
        public async Task<IActionResult> PaymentMercadoPago([FromBody] PaymentRequestDto paymentRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var result = await _paymentService.PaymentMercadoPago(paymentRequestDto);
                if(result ==null)
                    return StatusCode(409,"Problema ao processar pagamento");
                return Ok(result);
            }
            catch (FailureRequestException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }
    }
}
