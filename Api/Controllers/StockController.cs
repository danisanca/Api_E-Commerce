using ApiEstoque.Dto.Categories;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.Stock;
using Microsoft.AspNetCore.Authorization;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Shop;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public StockController(IStockService stockService, IProductService productService, IShopService shopService)
        {
            _stockService = stockService;
            _productService = productService;
            _shopService = shopService;
        }

        [SwaggerOperation(
        Summary = "Cria um registro de estoque",
        Description = "Rota responsavel pela primeiro registro do produto no estoque." +
            "Registro de Devolução" +
            "Saida de produto" +
            "Saida de produto pela venda" +
            "Acerto de Estoque" +
            "bloquear o estoque para o produto" +
            "Habilitar o estoque do produto"
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Houve um problema na criação do estoque.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Houve um problema na atualização do estoque.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Houve um problema na baixa do produto no estoque.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Tipo de Movimentação Invalida.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "O id do produto informado não pertence a você.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Estoque Atualizado")]
        [HttpPut]
        [Route("UpdateStock")]
        public async Task<ActionResult> Update([FromBody] StockUpdateDto stockUpdateDto)
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

                var findProduct = await _productService.GetById(stockUpdateDto.productId);
                if (findProduct == null)
                    throw new FailureRequestException(404, "O id do produto não Existe");
               
                if (findProduct.shopId.ToString() != stockUpdateDto.shopId)
                    throw new FailureRequestException(401, "O shop id informado é diferente do shop id cadastrado no produto.");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                if (stockUpdateDto.userId != userId)
                    throw new FailureRequestException(401, "O id do usuario informado não é o mesmo que esta authenticado.");

                var result = await _stockService.Update(stockUpdateDto);
                if (result == false) return NotFound();
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

        [SwaggerOperation(
       Summary = "Altera o status do estoque",
       Description = "Altera o status do estoque para 'Desabilitado' ou 'Ativo'.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do estoque nao localizado.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do produto nao localizado.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "O estoque ja está ativo")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "O estoque ja está desativado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Status Atualizado")]
        [HttpPut]
        [Route("ChangeStatusById")]
        public async Task<ActionResult> ChangeStatusById(StockChangeStatusDto model)
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

                var findStock = await _stockService.GetById(model.idStock);
                if (findStock == null) throw new FailureRequestException(401, "O id do estoque não existe");

                var findProduct = await _productService.GetById(findStock.productId);
                if (findProduct == null)
                    throw new FailureRequestException(401, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                bool result = await _stockService.ChangeStatusById(model);
                if (result == false)
                {
                    return NotFound();
                }
                else return Ok();
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
       Summary = "Buscar estoque por produto",
       Description = "Busca o estoque a partir do id do produto informado."
        )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não localizado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Estoque localizado", typeof(ShopDto))]
        [HttpGet]
        [Route("GetByProductId/{idProduct}")]
        public async Task<ActionResult> GetByProductId(Guid idProduct)
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
                if (findProduct == null) throw new FailureRequestException(404, "Produto não Localizado");
                var result = await _stockService.GetByProductId(idProduct);
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
