using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System.Collections.Generic;

namespace ApiEstoque.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper, IShopRepository shopRepository)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        public async Task<bool> ActiveCategories(int idCategories)
        {
            try
            {
                CategoriesModel category = await _categoriesRepository.GetCategoriesById(idCategories);
                if (category == null) throw new FailureRequestException(404, "Id da categoria não localizada.");
                if (category.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Categoria ja está ativa.");
                category.status = StandartStatus.Ativo.ToString();
                return await _categoriesRepository.UpdateCategories(category);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoriesDto> CreateCategories(CategoriesCreateDto categories)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(categories.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                CategoriesModel result = await _categoriesRepository.GetCategoriesByName(categories.name,categories.shopId);
                if (result != null) throw new FailureRequestException(409, "Categoria ja cadastrada.");
                var model = _mapper.Map<CategoriesModel>(categories);
                model.status = StandartStatus.Ativo.ToString();
                return _mapper.Map<CategoriesDto>(await _categoriesRepository.CreateCategories(model));

            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DisableCategories(int idCategories)
        {
            try
            {
                CategoriesModel category = await _categoriesRepository.GetCategoriesById(idCategories);
                if (category == null) throw new FailureRequestException(404, "Id da categoria não localizada.");
                if (category.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Categoria ja esta desabilitada");
                category.status = StandartStatus.Desabilitado.ToString();
                return await _categoriesRepository.UpdateCategories(category);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CategoriesDto>> GetAllCategories(int shopId,FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategories = await _categoriesRepository.GetAllCategories(shopId, status);
                if (findCategories == null) return new List<CategoriesDto>();
                return _mapper.Map<List<CategoriesDto>>(findCategories); 
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoriesDto> GetCategoriesById(int id)
        {
            try
            {
                var findCategory = await _categoriesRepository.GetCategoriesById(id);
                if (findCategory == null) throw new FailureRequestException(404, "Id da categoria nao localizada");
                return _mapper.Map<CategoriesDto>(findCategory);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); ;
            }
        }

        public async Task<CategoriesDto> GetCategoriesByName(string name, int shopId)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                var findCategory = await _categoriesRepository.GetCategoriesByName(name,shopId);
                if (findCategory == null) throw new FailureRequestException(404, "Nome da categoria nao localizada");
                return _mapper.Map<CategoriesDto>(findCategory);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateCategories(CategoriesUpdateDto categories)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(categories.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");
                CategoriesModel findCategory = await _categoriesRepository.GetCategoriesById(categories.idCategories);
                if (findCategory == null) throw new FailureRequestException(409, "Categoria com o id nao localizada.");
                var findName = await _categoriesRepository.GetCategoriesByName(categories.name,categories.shopId);
                if (findName != null) throw new FailureRequestException(409, "Categoria ja cadastrada com esse nome.");
                findCategory.name = categories.name;
                return await _categoriesRepository.UpdateCategories(findCategory);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
