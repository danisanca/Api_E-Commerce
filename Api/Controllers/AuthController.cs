using ApiEstoque.Dto.Login;
using ApiEstoque.Dto.User;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [SwaggerOperation(
        Summary = "Login",
        Description = "Realiza o login na aplicação"
         )]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Logado com sucesso", typeof(LoginResponse))]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var loginResult = await _authService.Login(user);
            if (loginResult.IsLogedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }


        [SwaggerOperation(
        Summary = "RefreshToken",
        Description = "Realiza a atualização do token ao informar o token antigo e o refresh token")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Usuário não autorizado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Token atualizado", typeof(LoginResponse))]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
        {
            var loginResult = await _authService.RefreshToken(model);
            if (loginResult.IsLogedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }
}
