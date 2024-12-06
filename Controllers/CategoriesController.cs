﻿using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost]
        [Route("CreateCategories")]
        public async Task<ActionResult> CreateCategories([FromBody] CategoriesCreateDto categoriesCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.CreateCategories(categoriesCreate);
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
        [Route("GetAllCategories/{shopId}")]
        public async Task<ActionResult> GetAllCategories(int shopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetAllCategories(shopId);
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
        [Route("GetAllActiveCategories/{shopId}")]
        public async Task<ActionResult> GetAllActiveCategories(int shopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetAllCategories(shopId,FilterGetRoutes.Ativo);
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
        [Route("GetAllDesactiveCategories/{shopId}")]
        public async Task<ActionResult> GetAllDesactiveCategories(int shopId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetAllCategories(shopId, FilterGetRoutes.Desabilitado);
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
        [Route("GetCategoryById/{IdCategory}")]
        public async Task<ActionResult> GetCategoryById(int IdCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetCategoriesById(IdCategory);
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
        [Route("GetCategoryByName/{categoryName}")]
        public async Task<ActionResult> GetCategoryByName([FromQuery] int shopId,string categoryName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.GetCategoriesByName(categoryName,shopId);
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
        [Route("UpdateCategories")]
        public async Task<ActionResult> UpdateCategories([FromBody] CategoriesUpdateDto categoryUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.UpdateCategories(categoryUpdate);
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
       
        [HttpPut]
        [Route("ActiveCategoriesById/{idCategory}")]
        public async Task<ActionResult> ActiveCategoriesById(int idCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.ActiveCategories(idCategory);
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

        [HttpPut]
        [Route("DisableCategoriesById/{idCategory}")]
        public async Task<ActionResult> DisableCategoriesById(int idCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _categoriesService.DisableCategories(idCategory);
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
    }
}