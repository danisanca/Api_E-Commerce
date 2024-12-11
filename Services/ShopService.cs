using ApiEstoque.Dto.Shop;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class ShopService : IShopService
    {
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;

        public ShopService(IMapper mapper, IShopRepository shopRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _shopRepository = shopRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> ActiveShop(int shopId)
        {
            try
            {
                var result = await _shopRepository.GetShopById(shopId);
                if (result == null) throw new FailureRequestException(404, "Id do shop não localizado.");
                if(result.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "O shop ja se encontra ativo.");
                result.status = StandartStatus.Ativo.ToString();
                return await _shopRepository.UpdateShop(result);
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

        public async Task<ShopDto> CreateShop(ShopCreateDto shopCreateDto)
        {
            try
            {
                var findUser = await _userRepository.GetUserById(shopCreateDto.userId);
                if (findUser == null) throw new FailureRequestException(404, "Usuario não localizado.");
                if (findUser.typeAccount == TypeUserEnum.Owner.ToString()) throw new FailureRequestException(409, "Usuario já possui uma loja.");
                
                ShopModel shop = _mapper.Map<ShopModel>(shopCreateDto);
                shop.status = StandartStatus.Ativo.ToString();
                await _shopRepository.UpdateShop(shop);
                findUser.typeAccount = TypeUserEnum.Owner.ToString();
                return _mapper.Map<ShopDto>(await _userRepository.UpdateUser(findUser));
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

        public async Task<bool> DisableShop(int shopId)
        {
            try
            {
                var result = await _shopRepository.GetShopById(shopId);
                if (result == null) throw new FailureRequestException(404, "Id da loja não localizado.");
                if (result.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "A loja ja se encontra desabilitado.");
                result.status = StandartStatus.Desabilitado.ToString();
                return await _shopRepository.UpdateShop(result);
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

        public async Task<List<ShopDto>> GetAllShops(FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                var findShop = await _shopRepository.GetAllShops(status);
                if (findShop == null) return new List<ShopDto>();
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

        public async Task<ShopDto> GetShopById(int id)
        {
            try
            {
                var findShop = await _shopRepository.GetShopById(id);
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

        public async Task<ShopDto> GetShopByName(string name)
        {
            try
            {
                var findShop = await _shopRepository.GetShopByName(name);
                if (findShop == null) throw new FailureRequestException(404, "Nome da loja não localizado.");
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

        public async Task<ShopDto> GetShopByUserId(int userId)
        {
            try
            {
                var findShop = await _shopRepository.GetShopByUserId(userId);
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
                var findShop = await _shopRepository.GetShopById(shopUpdateDto.shopId); 
                if (findShop == null) throw new FailureRequestException(404, "Nenhuma loja encontrar para o id");
                if (findShop.name == shopUpdateDto.name) throw new FailureRequestException(409, "O nome nao pode ser o mesmo que esta cadastrado.");
                findShop.name = shopUpdateDto.name;
                return await _shopRepository.UpdateShop(findShop);
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
