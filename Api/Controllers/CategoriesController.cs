using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.Login;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [SwaggerOperation(
        Summary = "Buscar lista de Categorias",
        Description = "Busca todas as categorias cadastradas.")]
        [SwaggerResponse(StatusCodes.Status200OK, "lista de cartegoria localizada", typeof(List<CategoriesDto>))]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetAll();
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
