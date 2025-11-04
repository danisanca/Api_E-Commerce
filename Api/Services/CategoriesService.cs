using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using SharedBase.Repository;
using System.Collections.Generic;

namespace ApiEstoque.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IBaseRepository<CategoriesModel> _baseRepository;
        private readonly ICategoriesRepository _categoriesRepository;
       
        private readonly IMapper _mapper;

        public CategoriesService(
            ICategoriesRepository categoriesRepository,
            IBaseRepository<CategoriesModel> baseRepository,
            IMapper mapper)
        {

            _baseRepository = baseRepository;
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }


        public async Task<List<CategoriesDto>> GetAll()
        {
            try
            {
                var findCategories = await _categoriesRepository.SelectAllByStatusAsync();
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

        public async Task<List<CategoriesDto>> GetAllByIds(List<Guid> Ids)
        {
            try
            {
                var findCategories = await _categoriesRepository.GetAllByIds(Ids);
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

        public async Task<CategoriesDto> GetById(Guid id)
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

        public async Task<CategoriesDto> GetByName(string name)
        {
            try
            {
                var findCategory = await _categoriesRepository.GetByName(name);
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


    }
}
