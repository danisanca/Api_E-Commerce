using System.Net;
using System.Security.Claims;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Shop;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Services;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public ProductController(IProductService productService,
            IShopService shopService,
            UserManager<UserModel> userManager
        )
        {
            _productService = productService;
            _shopService = shopService;
        }

        [SwaggerOperation(
        Summary = "Cadastrar um produto.",
        Description = "Cadastra um produto ao shop referente ao usuario logado"
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Loja não encontrada")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Produto ja cadastrado")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Falha no cadastrado")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto ja cadastrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto Cadastrado", typeof(ProductDto))]
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] ProductCreateDto productCreateDto)
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

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findOwnerShop.userId != userId) throw new FailureRequestException(401, "O id do shop informado não pertence a você.");

                ProductDto product = await _productService.Create(productCreateDto);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        Summary = "Atualizar Produto.",
        Description = "Atualiza o produto conforme dados enviados")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria nao localizada")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto Atualizado")]
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validação
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findProduct = await _productService.GetById(productUpdateDto.idProduct);
                if (findProduct == null)
                    throw new FailureRequestException(401, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                //Atualização
                bool result = await _productService.Update(productUpdateDto);
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
        Summary = "Altera o status do produto",
        Description = "Altera o status do produto para 'Desabilitado' ou 'Ativo'.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Status Atualizado")]
        [Authorize]
        [HttpPut]
        [Route("ChangeStatusById")]
        public async Task<ActionResult> ChangeStatusById(Guid idProduct, bool isActive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validação
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findProduct = await _productService.GetById(idProduct);
                if (findProduct == null)
                    throw new FailureRequestException(401, "O id do produto não Existe");

                var findOwnerShop = await _shopService.GetByUserId(userId);
                if (findProduct.shopId != findOwnerShop.id)
                    throw new FailureRequestException(401, "O id do produto informado não pertence a você.");

                //Atualização
                bool result = await _productService.ChangeStatus(idProduct, isActive);
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
        Summary = "Busca os produtos por loja.",
        Description = "Busca os produtos referente ao shop id informado.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de produtos retornada")]
        //[Authorize]
        [HttpGet]
        [Route("GetAllWithDetailsByIdShop/{shopId}")]
        public async Task<ActionResult> GetAllWithDetailsByIdShop(Guid shopId, [FromQuery] FilterGetRoutes status = FilterGetRoutes.All, [FromQuery] int limit = 20, [FromQuery] int page = 0, [FromQuery] string category = "")
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
               
                var findOwnerShop = await _shopService.GetById(shopId);
                if (findOwnerShop.userId != userId) throw new FailureRequestException(401, "O id do shop informado não pertence a você.");
                
                ProductViewModel products = await _productService.GetAllWithDetailsByIdShop(shopId,status, limit, page,category);
                if (products == null)
                {
                    return NotFound();
                }
                else return Ok(products);
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

        //Rotas Sem Authenticação
        [SwaggerOperation(
         Summary = "Busca todos os produtos Ativos.",
         Description = "Busca todos os produtos ativos de todas as lojas com as informações de disconto,imagem e estoque.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de produtos retornada")]
        [HttpGet]
        [Route("GetAllWithDetails")]
        public async Task<ActionResult> GetAllWithDetails([FromQuery] int limit = 20, [FromQuery] int page = 0, [FromQuery] string category = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductViewModel products = await _productService.GetAllWithDetails(limit, page, category);
                if (products == null)
                {
                    return NotFound();
                }
                else return Ok(products);
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
        Summary = "Busca todos os produtos ativos por nome",
        Description = "Busca todos os produtos ativos de todas as lojas com as informações de disconto,imagem e estoque com base na palavra informada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de produtos retornada")]
        [HttpGet]
        [Route("GetAllWithDetailsLikeName")]
        public async Task<ActionResult> GetAllWithDetailsLikeName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductViewModel product = await _productService.GetAllWithDetailsLikeName(name);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
         Summary = "Buscar o produto pelo id",
         Description = "Rota responsavel por buscar apenas um produto via id")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Produto não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Produto Encontrado", typeof(ProductDetailsDto))]
        [HttpGet]
        [Route("GetWithDetailsById/{idProduct}")]
        public async Task<ActionResult> GetWithDetailsById(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Atualização
                var result = await _productService.GetWithDetailsById(idProduct);
                if (result == null)
                {
                    return NotFound();
                }
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
