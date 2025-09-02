using ApiEstoque.Dto.Categories;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using System.Net;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.User;
using Microsoft.AspNetCore.Authorization;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Login;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public DiscountController(IDiscountService discountService, IProductService productService, IShopService shopService)
        {
            _discountService = discountService;
            _productService = productService;
            _shopService = shopService;
        }

        [SwaggerOperation(
        Summary = "Cria um desconto",
        Description = "Cadastra um desconto a um produto especifico referente a loja do usuario")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do produto nao existe")]
        [SwaggerResponse(StatusCodes.Status200OK, "Desconto Vinculado", typeof(DiscountDto))]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] DiscountCreateDto discountCreate)
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

                var findProduct = await _productService.GetById(discountCreate.productId);
                if (findProduct == null)
                    throw new FailureRequestException(404, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                var result = await _discountService.Create(discountCreate);
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

        [SwaggerOperation(
        Summary = "Atualiza um desconto",
        Description = "Atualiza o desconto vinculado a um produto especifico")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do produto não existe")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do desconto não existe")]
        [SwaggerResponse(StatusCodes.Status200OK, "Desconto Atualizado")]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] DiscountUpdateDto discountUpdate)
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

                var findDiscount = await _discountService.GetById(discountUpdate.id);
                if (findDiscount == null)
                    throw new FailureRequestException(404, "O id do desconto não existe");

                var findProduct = await _productService.GetById(findDiscount.productId);
                if (findProduct == null)
                    throw new FailureRequestException(404, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do desonto informado não pertence a você.");

                bool result = await _discountService.Update(discountUpdate);
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
        Summary = "Deleta um desconto",
        Description = "Deleta o desconto vinculado a um produto especifico a partir do id do desconto")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do produto não existe")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Id do desconto não existe")]
        [SwaggerResponse(StatusCodes.Status200OK, "Desconto Removido.")]
        [HttpDelete]
        [Route("DeleteById/{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
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

                var findDiscount = await _discountService.GetById(id);
                if (findDiscount == null)
                    throw new FailureRequestException(404, "O id do desconto não existe");

                var findProduct = await _productService.GetById(findDiscount.productId);
                if (findProduct == null)
                    throw new FailureRequestException(404, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do desonto informado não pertence a você.");

                bool result = await _discountService.DeleteById(id);
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
       
    }
}
