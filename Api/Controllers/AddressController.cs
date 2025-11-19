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
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(
        Summary = "Atualiza o endereço do usuario.",
        Description = "Atualiza o endereço do usuario conforme os dados informados."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Endereço não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Endereço Atualizado")]
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
                
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
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

    }
}
