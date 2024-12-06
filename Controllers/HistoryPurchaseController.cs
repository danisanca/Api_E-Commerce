using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.HistoryPurchase;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryPurchaseController : ControllerBase
    {
        private readonly IHistoryPurchaseService _historyPurchaseService;

        public HistoryPurchaseController(IHistoryPurchaseService historyPurchaseService)
        {
            _historyPurchaseService = historyPurchaseService;
        }

        [HttpPost]
        [Route("CreateHistoryPurchase")]
        public async Task<ActionResult> CreateHistoryPurchase([FromBody] HistoryPurchaseCreateDto historyPurchaseCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.CreateHistoryPurchase(historyPurchaseCreate);
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
        [Route("GetHistoryPurchaseById/{idHistoryPurchase}")]
        public async Task<ActionResult> GetHistoryPurchaseById(int idHistoryPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.GetHistoryPurchaseById(idHistoryPurchase);
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
        [Route("GetAllHistoryPurchaseByProductId/{idProduct}")]
        public async Task<ActionResult> GetAllHistoryPurchaseByProductId(int idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.GetAllHistoryPurchaseByProductId(idProduct);
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
        [Route("GetAllHistoryPurchaseByShopId/{idShop}")]
        public async Task<ActionResult> GetAllHistoryPurchaseByShopId(int idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.GetAllHistoryPurchaseByShopId(idShop);
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
