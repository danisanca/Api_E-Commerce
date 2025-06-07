using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Services;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreProductController : ControllerBase
    {
        private readonly IScoreProductService _scoreProductService;

        public ScoreProductController(IScoreProductService scoreProductService)
        {
            _scoreProductService = scoreProductService;
        }
        /*
        [HttpPut]
        [Route("UpdateScoreProduct")]
        public async Task<ActionResult> UpdateScoreProduct([FromBody] ScoreProductCreateDto scoreProductCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _scoreProductService.UpdateScore(scoreProductCreateDto);
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

        [HttpGet]
        [Route("GetScoreProductByProductId/{idProduct}")]
        public async Task<ActionResult> GetScoreProductByProductId(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _scoreProductService.GetScoreProductByProductId(idProduct);
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
        }*/
    }
}
