using ApiEstoque.Dto.Shop;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Dto.Product;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /*
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductDto product = await _productService.CreateProduct(productCreateDto);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("UpdateProductById")]
        public async Task<ActionResult> UpdateProductById([FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = await _productService.UpdateProduct(productUpdateDto);
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

        [HttpPut]
        [Route("ActiveProductById")]
        public async Task<ActionResult> ActiveProductById(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = await _productService.ActiveProduct(idProduct);
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

        [HttpPut]
        [Route("DisableProductById")]
        public async Task<ActionResult> DisableProductById(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = await _productService.DisableProduct(idProduct);
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

        [HttpGet]
        [Route("GetProductById/{idProduct}")]
        public async Task<ActionResult> GetProductById(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductDto produuct = await _productService.GetProductById(idProduct);
                if (produuct == null)
                {
                    return NotFound();
                }
                else return Ok(produuct);
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
        [Route("GetProductByName/{name}")]
        public async Task<ActionResult> GetProductByName(string name,[FromQuery] Guid shopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductDto product = await _productService.GetProductByName(name, shopId);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("GetAllProductByCategoryId/{idCategory}")]
        public async Task<ActionResult> GetAllProductByCategoryId(Guid idCategory, [FromQuery] Guid shopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ProductDto> product = await _productService.GetAllProductByCategoryId(idCategory, shopId);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("GetAllProductActivesByShopId/{idShop}")]
        public async Task<ActionResult> GetAllProductActives(Guid idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ProductDto> product = await _productService.GetAllProductsByShopId(idShop, FilterGetRoutes.Ativo);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("GetAllProductDisableByShopId/{idShop}")]
        public async Task<ActionResult> GetAllProductDisable(Guid idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ProductDto> product = await _productService.GetAllProductsByShopId(idShop, FilterGetRoutes.Desabilitado);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("GetAllProductByShopId/{idShop}")]
        public async Task<ActionResult> GetAllProduct(Guid idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ProductDto> product = await _productService.GetAllProductsByShopId(idShop);
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        [Route("GetProductFullById/{idProduct}")]
        public async Task<ActionResult> GetProductFullById(Guid idProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductFullDto produuct = await _productService.GetProductFullById(idProduct);
                if (produuct == null)
                {
                    return NotFound();
                }
                else return Ok(produuct);
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
        [Route("GetProductFullByName/{nameProduct}")]
        public async Task<ActionResult> GetProductFullByName(string nameProduct, Guid idShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductFullDto produuct = await _productService.GetProductFullByName(nameProduct, idShop);
                if (produuct == null)
                {
                    return NotFound();
                }
                else return Ok(produuct);
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
        [Route("GetAllProductsFullActive")]
        public async Task<ActionResult> GetAllProductsFullActive()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                List<ProductFullDto> product = await _productService.GetAllProductsFullActive();
                if (product == null)
                {
                    return NotFound();
                }
                else return Ok(product);
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
        */
    }
}
