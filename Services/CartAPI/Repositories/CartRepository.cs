using AutoMapper;
using CartAPI.Data;
using CartAPI.Dto;
using CartAPI.Models;
using SharedBase.Dtos.Cart;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApiContext _db;
        private readonly IMapper _mapper;

        public CartRepository(ApiContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CountCartDetailByCartHeaderId(Guid cartHeaderId)
        {
            return _db.CartDetail.Where(x => x.CartHeaderId == cartHeaderId).Count();
           
        }

        public async Task<List<CartDetail>> GetAllCartDetailsByCartHeaderId(Guid cartHeaderId)
        {
            return await _db.CartDetail.Where(x => x.CartHeaderId == cartHeaderId).ToListAsync();
        }

        public async Task<CartDto> GetByUserId(string userId)
        {
            // Carrega o CartHeader SEM tracking
            var cartHeader = await _db.CartHeader
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cartHeader == null)
                return new CartDto();

            // Carrega os detalhes SEM tracking
            var cartDetails = await _db.CartDetail
                .AsNoTracking()
                .Where(c => c.CartHeaderId == cartHeader.Id)
                .ToListAsync();

            // Monta retorno
            var cart = new CartDto
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                CartDetail = _mapper.Map<List<CartDetailDto>>(cartDetails)
            };

            return cart;
        }

        public async Task<CartDetail> GetCartDetailByProductIdAndCartHeaderId(Guid productId, Guid cartHeaderId)
        {
            return _db.CartDetail.FirstOrDefault(x => x.ProductId == productId && x.CartHeaderId == cartHeaderId);
        }
    }
}
