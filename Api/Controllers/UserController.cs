using System.Net;
using System.Security.Claims;
using ApiEstoque.Constants;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using MercadoPago.Resource.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [SwaggerOperation(
        Summary = "Criação de Usuario",
        Description = "Cria o usuario caso as informações estam validas e retorna um true."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status200OK,"Usuario Criado")]
        [HttpPost]
        [Route( "Create")]
        public async Task<ActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.Create(userCreateDto);
                if (user == null)
                {
                    return NotFound();
                }
                else return Ok(user);
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
        Summary = "Busca o usuário pelo id",
        Description = "Retorna os dados completos do usuário e o endereço caso o id informado seja o mesmo que está authenticado."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuário encontrado", typeof(UserDto))]
        [HttpGet]
        [Authorize]
        [Route("GetById/{idUser}")]
        public async Task<ActionResult> GetById(string idUser)
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

                var user = await _userService.GetById(idUser);
                if (user == null) return NotFound();
                if (user.id != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");
                
                return Ok(user);

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
        Summary = "Atualiza o usuario a partir do id do usuario.",
        Description = "Atualiza os dados do usuario caso nao haja conflito de informações"
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Usuário Atualizado")]
        [HttpPut]
        [Authorize]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
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

                var FindUser = await _userService.GetById(userUpdateDto.id);
                if (FindUser == null) return NotFound();
                if (FindUser.id != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");

                var user = await _userService.Update(userUpdateDto);
                if (user == false)
                {
                    return NotFound();
                }
                else return Ok(user);
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
        Summary = "Atualiza a senha do usuario.",
        Description = "Atualiza a senha do usuario a partir do informe da senha e confirmação da senha para validação. Caso as senhas estam corretas atualiza a senha."
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado / Sem Permissão ao Registro")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Senha Atualizado")]
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
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

                var FindUser = await _userService.GetById(changePasswordDto.userId);
                if (FindUser == null) return NotFound();
                if (FindUser.id != userId)
                    return StatusCode(401, "Você não tem permissão para acessar este registro.");

                var user = await _userService.ChangePassword(changePasswordDto);
                if (user == null)
                {
                    return NotFound();
                }
                else return Ok(user);
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
