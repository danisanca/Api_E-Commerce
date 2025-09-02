using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.Shop;
using Microsoft.AspNetCore.Authorization;
using ApiEstoque.Constants;
using ApiEstoque.Models;
using Microsoft.AspNetCore.Identity;
using ApiEstoque.Dto.Product;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly UserManager<UserModel> _userManager;

        public ShopController(IShopService shopService, UserManager<UserModel> userManager)
        {
            _shopService = shopService;
            _userManager = userManager;
        }

        [SwaggerOperation(
        Summary = "Cadastrar Loja",
        Description = "Cadastra uma loja para o usuario e atualiza as permissoes de propietario."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Usuario ja possui uma loja.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Falha no cadastrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loja Cadastrada", typeof(ShopDto))]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] ShopCreateDto shopCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var userId = User.FindFirst(ClaimTypeCustom.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findUser = await _userManager.FindByIdAsync(shopCreateDto.userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Usuario não localizado.");

                if (shopCreateDto.userId != userId)
                    throw new FailureRequestException(401, "O id informado não pertence a você.");

                ShopDto shop = await _shopService.CreateShop(shopCreateDto);
                if (shop == null)
                {
                    return NotFound();
                }
                else return Ok(shop);
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
        Summary = "Atualizar Loja",
        Description = "Atualiza a loja conforme os novos dados informados"
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Loja não encontrada")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Não é possivel atualizar para o mesmo nome")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loja Atualizada")]
        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] ShopUpdateDto shopUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = User.FindFirst(ClaimTypeCustom.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findShop = await _shopService.GetById(shopUpdateDto.shopId);
                if (findShop == null) return NotFound();
                if (findShop.userId != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");

                bool result = await _shopService.UpdateShop(shopUpdateDto);
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
        Summary = "Altera status da loja",
        Description = "Altera o status do produto para 'Desabilitado' ou 'Ativo'."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Loja não encontrada")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "A Loja ja está ativa")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "A Loja ja está desativada")]
        [SwaggerResponse(StatusCodes.Status200OK, "Status Atualizado")]
        [HttpPut]
        [Route("ChangeStatusById")]
        public async Task<ActionResult> ChangeStatusById(Guid idShop,bool isActive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = User.FindFirst(ClaimTypeCustom.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                var findShop = await _shopService.GetById(idShop);
                if (findShop == null) return NotFound();
                if (findShop.userId != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");

                bool result = await _shopService.ChangeStatusById(idShop,isActive);
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
        Summary = "Buscar loja por usuario",
        Description = "Busca a loja a partir do id do usuario informado."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Loja não encontrada")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loja localizada", typeof(ShopDto))]
        [HttpGet]
        [Route("GetByUserId/{idUser}")]
        public async Task<ActionResult> GetByUserId(string idUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var findUser = await _userManager.FindByIdAsync(idUser.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Usuario não localizado.");

                var userId = User.FindFirst(ClaimTypeCustom.Id)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                ShopDto shop = await _shopService.GetByUserId(idUser);
                if (shop == null) return NotFound();
                if (shop.userId != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");

                return Ok(shop);
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
