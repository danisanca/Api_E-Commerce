using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;

        public DiscountService(IMapper mapper, IDiscountRepository discountRepository,
            IProductRepository productRepository, IShopRepository shopRepository)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
        }

        public async Task<DiscountDto> CreateDiscount(DiscountCreateDto discountCreate)
        {
            try
            {
                ProductModel findProduct = await _productRepository.GetProductById(discountCreate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id da produto não localizada.");
                DiscountModel findDiscount = await _discountRepository.GetDiscountByProductId(findProduct.id);
                if (findDiscount != null) throw new FailureRequestException(404, "Ja existe um desconto para esse produto.");
                var model = _mapper.Map<DiscountModel>(discountCreate);
                return _mapper.Map<DiscountDto>(await _discountRepository.AddDiscount(model));
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

        public async Task<bool> DeleteDiscountByProductId(int idProduct)
        {
            try
            {
                DiscountModel findDiscount = await _discountRepository.GetDiscountByProductId(idProduct);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto para esse produto.");
                return await _discountRepository.DeleteDiscount(findDiscount);
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

        public async Task<DiscountDto> GetDiscountByProductId(int idProduct)
        {
            try
            {
                ProductModel findProduct = await _productRepository.GetProductById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id da produto não localizada.");
                DiscountModel findDiscount = await _discountRepository.GetDiscountByProductId(findProduct.id);
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

        public async Task<bool> UpdateDiscountByProductId(DiscountUpdateDto discountUpdate)
        {
            try
            {

                ProductModel findProduct = await _productRepository.GetProductById(discountUpdate.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id da produto não localizada.");
                DiscountModel findDiscount = await _discountRepository.GetDiscountByProductId(findProduct.id);
                if (findDiscount == null) throw new FailureRequestException(404, "Não existe um desconto para esse produto.");
                if (discountUpdate.percentDiscount != null)
                {
                    findDiscount.percentDiscount = (float)discountUpdate.percentDiscount;
                }
                if (discountUpdate.description != null)
                {
                    findDiscount.description = discountUpdate.description;
                }
                findDiscount.updatedAt = DateTime.UtcNow;
                return await _discountRepository.UpdateDiscount(findDiscount);
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


        public async Task<List<DiscountDto>> GetAllDiscountsByShopId(int idShop)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop não localizado.");
                var findDiscount = await _discountRepository.GetAllDiscountsByShopId(idShop);
                if (findDiscount == null) throw new FailureRequestException(404, "Não há discountos cadastrados ainda.");
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
