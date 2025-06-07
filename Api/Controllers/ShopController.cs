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

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
       

        [HttpPost]
        [Route("CreateShop")]
        public async Task<ActionResult> CreateShop([FromBody] ShopCreateDto shopCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
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

        

        [HttpGet]
        [Route("GetById/{idShop}")]
        public async Task<ActionResult> GetById(Guid idShop)
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

                return Ok(findShop);
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
