using ApiEstoque.Dto.Stock;
using ApiEstoque.Services.Exceptions;
using System.Net;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiEstoque.Dto.Adress;
using ApiEstoque.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApiEstoque.Constants;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        
        [HttpPut]
        [Authorize]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] AddressUpdateDto addressUpdate)
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

                
                var address = await _addressService.GetById(addressUpdate.id);
                if (address == null)
                    return NotFound("Endereço não encontrado.");

                if (address.userId != userId)
                    return StatusCode(401,"Você não tem permissão para alterar este endereço.");

                
                var result = await _addressService.Update(addressUpdate);

                if (!result)
                    return NotFound();

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

        

        [HttpGet]
        [Authorize]
        [Route("GetByUserId/{idUser}")]
        public async Task<ActionResult> GetByUserId(string idUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _addressService.GetByUserId(idUser);
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
