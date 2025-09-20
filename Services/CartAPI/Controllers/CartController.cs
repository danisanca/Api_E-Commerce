using System.Net;
using CartAPI.Dto;
using CartAPI.Helpers.Exceptions;
using CartAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
    }
}
