using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Dto.PaymentRequest;
using ApiEstoque.Dto.User;

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
        public async Task<ActionResult> CreateHistoryPurchase([FromBody] PaymentRequestDto model, string ext_ref)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.CreatePendingPurchase(model, ext_ref);
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
        [Route("GetHistoryPurchaseByExternalRefId/{external_ref}")]
        public async Task<ActionResult> GetHistoryPurchaseByExternalRefId(string external_ref)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.GetHistoryPurchaseByExternalRefId(external_ref);
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
        [Route("GetAllHistoryPurchaseByUserId/{idUser}")]
        public async Task<ActionResult> GetAllHistoryPurchaseByUserId(int idUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _historyPurchaseService.GetAllHistoryPurchaseByUserId(idUser);
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
        [Route("UpdateHistoryPurchase")]
        public async Task<ActionResult> UpdateHistoryPurchase(string external_ref, string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = await _historyPurchaseService.UpdateHistoryPurchaseByExternalRef(external_ref, status);
                if (result == false)
                {
                    return NotFound();
                }
                else return Ok();
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
