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
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginController(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _loginService.Login(user);

                if (result != null)
                {
                    dynamic jsonResult = JsonConvert.DeserializeObject(System.Text.Json.JsonSerializer.Serialize(result));
                    if (jsonResult.authenticated == false)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized, "Email e/ou Senha inválida.");
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                //alterar o retorno da msg
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Authorize("Bearer")]
        [HttpGet]
        [Route("TesteToken_GetUserById/{idUser}")]
        public async Task<ActionResult> TesteToken_GetUserById(int idUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDto user = await _userService.GetUserById(idUser);
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
