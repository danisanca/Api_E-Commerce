using System.Security.Policy;
using ApiEstoque.Constants;
using ApiEstoque.Dto.Shop;
using ApiEstoque.Models;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using MercadoPago.Resource.User;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoque.Services
{
    public class ShopService : IShopService
    {
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly UserManager<UserModel> _userManager;
        private readonly IBaseRepository<ShopModel> _baseRepository;
        private readonly IUserService _userService;

        public ShopService(IMapper mapper, 
            IShopRepository shopRepository, 
            UserManager<UserModel> userManager, 
            IBaseRepository<ShopModel> baseRepository, 
            IUserService userService)
        {
            _mapper = mapper;
            _shopRepository = shopRepository;
            _userManager = userManager;
            _baseRepository = baseRepository;
            _userService = userService;

        }
        //TODO: Implementar logica do ModelUserSeller
        public async Task<ShopDto> CreateShop(ShopCreateDto shopCreateDto)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(shopCreateDto.userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Usuario não localizado.");

                var userHasShop = await _shopRepository.GetByUserId(shopCreateDto.userId.ToString());
                if(userHasShop != null) throw new FailureRequestException(404, "Usuario ja possui uma loja");

                ShopModel shop = _mapper.Map<ShopModel>(shopCreateDto);
                await _baseRepository.InsertAsync(shop);

                var setRole = await _userService.SetSellerRole(findUser.Id);
                if (setRole != true) throw new FailureRequestException(404, "Falha ao configurar usuario");

                return _mapper.Map<ShopDto>(shop);
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

        public async Task<bool> ChangeStatusById(Guid shopId, bool isActive)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(shopId);
                if (result == null) throw new FailureRequestException(404, "Id do shop não localizado.");

                if (isActive == true)
                {
                    if (result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(404, "Shop já esta ativo.");
                    result.status = StandartStatus.Ativo.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }
                else
                {
                    if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(404, "Shop já esta desativado.");
                    result.status = StandartStatus.Desabilitado.ToString();
                    return await _baseRepository.UpdateAsync(result);
                }

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


        public async Task<ShopDto> GetById(Guid id)
        {
            try
            {
                var findShop = await _baseRepository.SelectByIdAsync(id);
                if(findShop == null) throw new FailureRequestException(404, "Id da loja não localizado.");
                return _mapper.Map<ShopDto>(findShop);
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

        public async Task<ShopDto> GetByUserId(string userId)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Usuario não localizado.");

                var findShop = await _shopRepository.GetByUserId(userId);
                if (findShop == null) throw new FailureRequestException(404, "Nenhuma loja encontrar para o id do usuario");
                return _mapper.Map<ShopDto>(findShop);
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

        public async Task<bool> UpdateShop(ShopUpdateDto shopUpdateDto)
        {
            try
            {
                var findShop = await _baseRepository.SelectByIdAsync(shopUpdateDto.shopId); 
                if (findShop == null) throw new FailureRequestException(404, "Nenhuma loja encontrar para o id");
                if (findShop.name == shopUpdateDto.name) throw new FailureRequestException(409, "O nome nao pode ser o mesmo que esta cadastrado.");
                findShop.name = shopUpdateDto.name;
                return await _baseRepository.UpdateAsync(findShop);
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

        public async Task<List<ShopDto>> GetAllByIds(List<Guid> ids)
        {
            try
            {
                var findShop = await _shopRepository.GetAllByIds(ids);
                if (findShop == null) throw new FailureRequestException(404, "Nenhuma loja encontrar para os ids.");
                return _mapper.Map<List<ShopDto>>(findShop);
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
