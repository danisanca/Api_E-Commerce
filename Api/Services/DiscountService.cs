using System.Collections.Generic;
using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using SharedBase.Repository;

namespace ApiEstoque.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly IShopService _shopService;
        private readonly IBaseRepository<DiscountModel> _baseRepository;

        public DiscountService(IMapper mapper, IDiscountRepository discountRepository,
            
           IShopService shopService, IBaseRepository<DiscountModel> baseRepository)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _shopService = shopService;
            _baseRepository = baseRepository;
        }

        public async Task<DiscountDto> Create(DiscountCreateDto discountCreate)
        {
            try
            {
                DiscountModel findDiscount = await _discountRepository.GetByProductId(discountCreate.productId);
                if (findDiscount != null) throw new FailureRequestException(404, "Ja existe um desconto para esse produto.");
                var model = _mapper.Map<DiscountModel>(discountCreate);
                return _mapper.Map<DiscountDto>(await _baseRepository.InsertAsync(model));
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

        public async Task<bool> DeleteById(Guid id)
        {
            try
            {
                DiscountModel findDiscount = await _baseRepository.SelectByIdAsync(id);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto com esse id.");
                return await _baseRepository.DeleteAsync(findDiscount.Id);
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

        
        public async Task<bool> Update(DiscountUpdateDto discountUpdate)
        {
            try
            {
                DiscountModel findDiscount = await _baseRepository.SelectByIdAsync(discountUpdate.id);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto para esse produto.");
                if (discountUpdate.percentDiscount != null)
                {
                    findDiscount.percentDiscount = (float)discountUpdate.percentDiscount;
                }
                findDiscount.UpdatedAt = DateTime.Now;
                return await _baseRepository.UpdateAsync(findDiscount);
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


        public async Task<DiscountDto> GetByProductId(Guid idProduct)
        {
            try
            {
                DiscountModel findDiscount = await _discountRepository.GetByProductId(idProduct);
                if (findDiscount == null) return new DiscountDto();
                return _mapper.Map<DiscountDto>(findDiscount);
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


        public async Task<DiscountDto> GetById(Guid id)
        {
            try
            {
                DiscountModel findDiscount = await _baseRepository.SelectByIdAsync(id);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto para esse produto.");
                return _mapper.Map<DiscountDto>(findDiscount);
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

        public async Task<List<DiscountDto>> GetAllByProductsIds(List<Guid> ids)
        {
            try
            {
                List<DiscountModel> findDiscount = await _discountRepository.GetAllByProductsIds(ids);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto para esses ids.");
                return _mapper.Map<List<DiscountDto>>(findDiscount);
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
