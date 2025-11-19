using System.Reflection.PortableExecutable;
using AutoMapper;
using CartAPI.Dto;
using CartAPI.Helpers.Exceptions;
using CartAPI.Models;
using CartAPI.Repositories;
using CartAPI.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedBase.Dtos.Cart;
using SharedBase.Repository;
namespace CartAPI.Services
{
    public class CartService:ICartService
    {
        private readonly IBaseRepository<CartDetail> _baseCartDetail;
        private readonly IBaseRepository<CartHeader> _baseCartHeader;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(IBaseRepository<CartDetail> baseCartDetail, IBaseRepository<CartHeader> baseCartHeader, IMapper mapper, ICartRepository cartRepository)
        {
            _baseCartDetail = baseCartDetail;
            _baseCartHeader = baseCartHeader;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

       

        public async Task<CartDto> Create(CartCreateDto cartModel)
        {
            try
            {
                CartDto cart = await _cartRepository.GetByUserId(cartModel.UserId);
                if (cart.CartHeader == null) {
                    
                        var cartHeader = await _baseCartHeader.InsertAsync(new CartHeader()
                        {
                            Id = Guid.NewGuid(),
                            UserId = cartModel.UserId
                        });
                    var cartDetails = new List<CartDetail>
                        {
                            new CartDetail
                            {
                                Id = Guid.NewGuid(),
                                CartHeaderId = cartHeader.Id,
                                Count = cartModel.item.Count,
                                ProductId = cartModel.item.ProductId,
                                Discount = cartModel.item.Discount,
                                Price = cartModel.item.Price,
                                ProductName = cartModel.item.ProductName,
                                Description = cartModel.item.Description,
                            }
                        };


                     var reuslt = await _baseCartDetail.InsertRangeAsync(cartDetails);

                        CartDto cartResult = new()
                        {
                            CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                            CartDetail = _mapper.Map<List<CartDetailDto>>(cartDetails)
                        };

                        return cartResult;
                    
                }
                else throw new FailureRequestException(409, "Você ja tem um carrinho. Não é possivel recriar.");
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }

        }

        public async Task<CartDto> GetByUserId(string userId)
        {
            try
            {
                CartDto cart = await _cartRepository.GetByUserId(userId);

                if (cart.CartHeader == null)
                    return new CartDto();

                else return cart;
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(CartUpdateDto cartModel)
        {
            
            try
            {
                var findCartHeader = await _baseCartHeader.SelectByIdAsync(cartModel.CartHeaderId);
                if (findCartHeader == null)
                {
                    throw new FailureRequestException(404, "Carrinho não encontrado");
                }
                // 1. Buscar todos os itens atuais no banco
                var currentDetails = await _cartRepository.GetAllCartDetailsByCartHeaderId(cartModel.CartHeaderId);


                

                // 4. Atualizar ou inserir os itens enviados
             
                    var existingDetail = currentDetails.FirstOrDefault(cd => cd.ProductId == cartModel.item.ProductId);

                    if (existingDetail != null)
                    {
                        // Atualiza a quantidade
                        existingDetail.Count = cartModel.item.Count;
                        await _baseCartDetail.UpdateAsync(existingDetail);
                    }
                    else
                    {
                        // Insere novo
                        var newDetail = new CartDetail
                        {
                            Id = Guid.NewGuid(),
                            CartHeaderId = findCartHeader.Id,
                            ProductId = cartModel.item.ProductId,
                            Count = cartModel.item.Count,
                            Discount = cartModel.item.Discount,
                            Price = cartModel.item.Price,
                            ProductName = cartModel.item.ProductName,
                            Description = cartModel.item.Description,
                        };
                        await _baseCartDetail.InsertAsync(newDetail);
                    }
                

                return true;
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Guid cartDeatilId)
        {
            try
            {
                var findCartDetail = await _baseCartDetail.SelectByIdAsync(cartDeatilId);
                if (findCartDetail == null) throw new FailureRequestException(404, "Item do carrinho não encontrado.");
                var countCartDetails = await _cartRepository.CountCartDetailByCartHeaderId(findCartDetail.CartHeaderId);
                if (countCartDetails > 1) await _baseCartDetail.DeleteAsync(cartDeatilId);
                else await _baseCartHeader.DeleteAsync(findCartDetail.CartHeaderId);
                return true;
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Clear(Guid cartHeaderId)
        {
            try
            {
                var findCartHeader = await _baseCartHeader.SelectByIdAsync(cartHeaderId);
                if (findCartHeader == null)
                {
                    throw new FailureRequestException(404, "Carrinho não encontrado");
                }
                return await _baseCartHeader.DeleteAsync(cartHeaderId);
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CartHeaderDto> GetCartHeaderByCartDetailId(Guid cartDeatilId)
        {
            try
            {
                var cartDetail = await _baseCartDetail.SelectByIdAsync(cartDeatilId);
                if (cartDetail == null) throw new FailureRequestException(404, "CartDetailId não existe.");
                return _mapper.Map<CartHeaderDto>(await _baseCartHeader.SelectByIdAsync(cartDetail.CartHeaderId));

            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<CartHeaderDto> GetCartHeaderId(Guid cartHeaderId)
        {
            try
            {
                var carHeader = await _baseCartHeader.SelectByIdAsync(cartHeaderId);
                if (carHeader == null) throw new FailureRequestException(404, "CartDetailId não existe.");
                return _mapper.Map<CartHeaderDto>(carHeader);

            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
