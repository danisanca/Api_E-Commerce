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
        private readonly IOrderServices _orderServices;

        public PaymentController(IPaymentService paymentService, IOrderServices orderServices)
        {
            _paymentService = paymentService;
            _orderServices = orderServices;
        }

        [SwaggerOperation(
           Summary = "Pagamento Com Mercado Pago",
           Description = "Rota responsavel por realizar o pagamento do pedido via mercado pago."
         )]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Problemas para processar pagamento.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrilho Localizado.", typeof(MercadoPagoResult))]
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

        [SwaggerOperation(
        Summary = "Buscar order de pagamento",
        Description = "Rota responsavel por trazer o carrinho de compras do usuario a partir do id do usuario."
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "order de pagamento nao localizada")]
        [SwaggerResponse(StatusCodes.Status200OK, "Order Localizada.", typeof(OrderHeaderDto))]
        [HttpGet]
        [Route("GetByOrderToPayment/{headerId}")]
        public async Task<ActionResult> GetByOrderToPayment(Guid headerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var finduserId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(finduserId))
                    return Unauthorized("Usuário não autenticado.");

                var cart = await _orderServices.GetHeaderById(headerId);
                if (cart == null) return NotFound();

                if (finduserId != cart.UserId)
                    return StatusCode(401, "Você não tem permissão para manipular este registro.");

                return Ok(cart);
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
