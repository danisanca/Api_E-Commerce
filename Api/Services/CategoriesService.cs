using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System.Collections.Generic;

namespace ApiEstoque.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IBaseRepository<CategoriesModel> _baseRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IBaseRepository<ShopModel> _shopRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoriesRepository,
            IBaseRepository<CategoriesModel> _baseRepository,
            IMapper mapper, IBaseRepository<ShopModel> shopRepository)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        public async Task<bool> ActiveCategories(int idCategories)
        {
            try
            {
                CategoriesModel category = await _baseRepository.SelectByIdAsync(idCategories);
                if (category == null) throw new FailureRequestException(404, "Id da categoria não localizada.");
                if (category.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Categoria ja está ativa.");
                category.status = StandartStatus.Ativo.ToString();
                return await _baseRepository.UpdateAsync(category);
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
                
                CategoriesModel result = await _categoriesRepository.GetCategoriesByName(categories.name);
                if (result != null) throw new FailureRequestException(409, "Categoria ja cadastrada.");
                var model = _mapper.Map<CategoriesModel>(categories);
                model.status = StandartStatus.Ativo.ToString();
                return _mapper.Map<CategoriesDto>(await _baseRepository.InsertAsync(model));

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
                CategoriesModel category = await _baseRepository.SelectByIdAsync(idCategories);
                if (category == null) throw new FailureRequestException(404, "Id da categoria não localizada.");
                if (category.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Categoria ja esta desabilitada");
                category.status = StandartStatus.Desabilitado.ToString();
                return await _baseRepository.UpdateAsync(category);
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

        public async Task<List<CategoriesDto>> GetAllCategories(FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                
                var findCategories = await _categoriesRepository.GetAllCategories(status);
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
                var findCategory = await _baseRepository.SelectByIdAsync(id);
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

        public async Task<CategoriesDto> GetCategoriesByName(string name)
        {
            try
            {
                
                var findCategory = await _categoriesRepository.GetCategoriesByName(name);
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
                
                CategoriesModel findCategory = await _baseRepository.SelectByIdAsync(categories.idCategories);
                if (findCategory == null) throw new FailureRequestException(409, "Categoria com o id nao localizada.");
                var findName = await _categoriesRepository.GetCategoriesByName(categories.name);
                if (findName != null) throw new FailureRequestException(409, "Categoria ja cadastrada com esse nome.");
                findCategory.name = categories.name;
                return await _baseRepository.UpdateAsync(findCategory);
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
