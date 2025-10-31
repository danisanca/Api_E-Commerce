using System.Net;
using System.Security.Claims;
using CartAPI.Dto;
using CartAPI.Helpers.Exceptions;
using CartAPI.Services;
using CartAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedBase.Constants.RabbitMQ;
using SharedBase.Dtos.Cart;
using SharedBase.Dtos.RabbitMq;
using Swashbuckle.AspNetCore.Annotations;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private IRabbitMqMessageSender _rabbitMQMessageSender;

        public CartController(ICartService cartService, IRabbitMqMessageSender rabbitMQMessageSender)
        {
            _cartService = cartService;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [SwaggerOperation(
         Summary = "Criação do Carrinho",
         Description = "Caso o usuario nao tenha um carrinho a rota é responsavel por criar um CartHeader para salvar os itens do carrinho."
        )]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Você ja tem um carrinho. Não é possivel recriar.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrinho Criado", typeof(CartDto))]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] CartCreateDto cartModel)
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

                if (userId != cartModel.UserId )
                    return StatusCode(401, "Você não tem permissão para manipular este registro."); 

                var cart = await _cartService.Create(cartModel);
                if(cart == null) return NotFound();
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

        [SwaggerOperation(
         Summary = "Buscar o carrinho do usuario.",
         Description = "Rota responsavel por trazer o carrinho de compras do usuario a partir do id do usuario."
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuario não possui um carrinho de compras.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrilho Localizado.", typeof(CartDto))]
        [HttpGet]
        [Route("GetById/{userId}")]
        public async Task<ActionResult> GetByUserId(string userId)
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

                if (finduserId != userId)
                    return StatusCode(401, "Você não tem permissão para manipular este registro.");

                var cart = await _cartService.GetByUserId(userId);
                if (cart.CartHeader == null) return NotFound();
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

        [SwaggerOperation(
        Summary = "Atualizar Carrinho",
        Description = "Rota responsavel por atualizar a lista de produtos conforme listagem passada."
         )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Carrinho não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrinho Atualizado")]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] CartUpdateDto cartModel)
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

                if (finduserId != cartModel.UserId)
                    return StatusCode(401, "Você não tem permissão para manipular este registro.");

                var cart = await _cartService.Update(cartModel);
                if (cart == false) return NotFound();
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

        [SwaggerOperation(
        Summary = "Deleta item do carrinho",
        Description = "Rota responsavel por deletar os itens do carrinho e quando tiver apenas 1 item deleta o cartHeader tambem."
         )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Item do carrinho não encontrado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Item deletado.")]
        [HttpDelete]
        [Route("DeleteItemById/{cartDetailId}")]
        public async Task<ActionResult> DeleteItemById(Guid cartDetailId)
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

                var findUserByCartDetail = await _cartService.GetCartHeaderByCartDetailId(cartDetailId);
                if (finduserId != findUserByCartDetail.UserId)
                    return StatusCode(401, "Você não tem permissão para manipular este registro.");

                var cart = await _cartService.Delete(cartDetailId);
                if (cart == false) return NotFound();
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

        [SwaggerOperation(
        Summary = "Limpa os itens do carrinho",
        Description = "Rota responsavel por deletar todos os itens do carrinho juntamento com cartHeader."
         )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Carrinho não encontrado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrinho limpo.")]
        [HttpDelete]
        [Route("ClearCart/{cartHeaderId}")]
        public async Task<ActionResult> ClearCart(Guid cartHeaderId)
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

                var CartHeader = await _cartService.GetCartHeaderId(cartHeaderId);
                if (finduserId != CartHeader.UserId)
                    return StatusCode(401, "Você não tem permissão para manipular este registro.");

                var cart = await _cartService.Clear(cartHeaderId);
                if (cart == false) return NotFound();
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

        [SwaggerOperation(
        Summary = "Checkout da Compra",
        Description = "Rota responsavel por realizar o checkout da compra e manda o carrinho para o rabbitMq"
         )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Carrinho não encontrado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Carrinho limpo.")]
        [HttpPost]
        [Route("CheckOut")]
        public async Task<ActionResult<CheckOutCartMsgDto>> CheckOut()
        {
            var finduserId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(finduserId))
                return Unauthorized("Usuário não autenticado.");

            var cart = await _cartService.GetByUserId(finduserId);
            if (cart == null) return StatusCode(404, "Nenhum Registro Localizado");

            if (finduserId != cart.CartHeader.UserId)
                return StatusCode(401, "Você não tem permissão para manipular este registro.");

            CheckOutCartMsgDto checkOut = new CheckOutCartMsgDto();

            checkOut.Id = cart.CartHeader.Id;
            checkOut.UserId = cart.CartHeader.UserId;
            checkOut.ListCount = cart.CartDetail.Count();
            checkOut.CartDetail = cart.CartDetail;

            _rabbitMQMessageSender.SendMessage(checkOut, ConfigRabbitMq.checkOutQueue);
            //await _cartService.Clear(cart.CartHeader.Id);

            return Ok(checkOut);
        }
    }
}

 /*
 / Cart
 1° checkoutqueue - Gerada pelo carrinho
 / Order
 2° orderpaymentprocessqueue - Gerada pelo orderm
/ Payment
 3° orderpaymentprocessqueue - Consome a queue da order
 Devolve uma orderpaymentresultqueue
/ Order
 4° orderpaymentresultqueue - Consome o status e atualiza a ordem processada pelo serviço de pagamento.
 */

