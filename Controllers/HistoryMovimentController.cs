using ApiEstoque.Dto.Categories;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.HistoryMoviment;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryMovimentController : ControllerBase
    {
        private readonly IHistoryMovimentService _historyMovimentService;

        public HistoryMovimentController(IHistoryMovimentService historyMovimentService)
        {
            _historyMovimentService = historyMovimentService;
        }

        [HttpPost]
        [Route("CreateHistoryMoviment")]
        public async Task<ActionResult> CreateHistoryMoviment([FromBody] HistoryMovimentCreateDto historyMovimentCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyMovimentService.CreateHistoryMoviment(historyMovimentCreate);
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
        [Route("GetHistoryMovimentById/{idHistoryMoviment}")]
        public async Task<ActionResult> GetHistoryMovimentById(int idHistoryMoviment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyMovimentService.GetHistoryMovimentById(idHistoryMoviment);
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
        [Route("GetAllHistoryMovimentByProductId/{idProduct}")]
        public async Task<ActionResult> GetAllHistoryMovimentByProductId(int idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyMovimentService.GetAllHistoryMovimentByProductId(idProduct);
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
        [Route("GetAllHistoryMovimentByShopId/{idShop}")]
        public async Task<ActionResult> GetAllHistoryMovimentByShopId(int idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyMovimentService.GetAllHistoryMovimentByShopId(idShop);
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
