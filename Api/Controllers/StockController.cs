using ApiEstoque.Dto.Categories;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.Stock;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [Route("CreateStock")]
        public async Task<ActionResult> CreateStock([FromBody] StockCreateDto stockCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.CreateStock(stockCreateDto);
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
        [HttpPut]
        [Route("UpdateStock")]
        public async Task<ActionResult> UpdateStock([FromBody] StockUpdateDto stockUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.UpdateStock(stockUpdateDto);
                if (result == false) return NotFound();
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
        [HttpDelete]
        [Route("DeleteStock/{idStock}")]
        public async Task<ActionResult> DeleteStock(int idStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.DeleteStockById(idStock);
                if (result == false) return NotFound();
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
        [Route("GetAllStockByShopId/{idShop}")]
        public async Task<ActionResult> GetAllStockByShopId(int idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.GetAllStockByShopId(idShop);
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
        [Route("GetStockById/{idStock}")]
        public async Task<ActionResult> GetStockById(int idStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.GetStockById(idStock);
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
        [Route("GetStockByProductId/{idProduct}")]
        public async Task<ActionResult> GetStockByProductId(int idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _stockService.GetStockByProductId(idProduct);
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
