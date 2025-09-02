using ApiEstoque.Dto.Categories;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.HistoryMoviment;
using Microsoft.AspNetCore.Authorization;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryMovimentController : ControllerBase
    {
        private readonly IHistoryMovimentService _historyMovimentService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public HistoryMovimentController(IHistoryMovimentService historyMovimentService,
            IShopService shopService, IProductService productService)
        {
            _historyMovimentService = historyMovimentService;
             _productService = productService;
            _shopService = shopService;
        }


        [SwaggerOperation(
        Summary = "Historico de Movimentações",
        Description = "Busca o historico de movimentaçõs do produto a partir do id.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do produto não existe")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de movimentações localizada",typeof(List<HistoryMovimentDto>))]
        [HttpGet]
        [Route("GetAllByProductId/{idProduct}")]
        public async Task<ActionResult> GetAllByProductId(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validação
                var userId = User.FindFirst(ClaimTypeCustom.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findProduct = await _productService.GetById(idProduct);
                if (findProduct == null)
                    throw new FailureRequestException(404, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                var result = await _historyMovimentService.GetAllByProductId(idProduct);
                if (result == null) return NotFound();
                else return Ok(result);
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
