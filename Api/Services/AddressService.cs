using ApiEstoque.Dto.Adress;
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
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IBaseRepository<AddressModel> _baseRepository;
        private readonly UserManager<UserModel> _userManager;

        public AddressService(IMapper mapper,
            IAddressRepository addressRepository, 
            IBaseRepository<AddressModel> baseRepository,
            UserManager<UserModel> userManager
            )
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _baseRepository = baseRepository;
            _userManager = userManager;
        }

        public async Task<AddressDto> Create(AddressCreateDto addressCreate)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(addressCreate.userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Não existe usuario com esse id");
                var model = _mapper.Map<AddressModel>(addressCreate);
                return _mapper.Map<AddressDto>(await _baseRepository.InsertAsync(model));
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

        public async Task<AddressDto> GetById(Guid id)
        {
            try
            {
                var result = await _baseRepository.SelectByIdAsync(id);
                if (result == null) throw new FailureRequestException(404, "Não existe endereço com esse id");
                return _mapper.Map<AddressDto>(result);
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

        public async Task<AddressDto> GetByUserId(string userId)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(userId);
                if (findUser == null) throw new FailureRequestException(404, "Não existe usuario com esse id");
                return _mapper.Map<AddressDto>(await _addressRepository.GetByUserId(findUser.Id));
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

        public async Task<bool> Update(AddressUpdateDto addressUpdate)
        {
            try
            {
                var findAddress = await _baseRepository.SelectByIdAsync(addressUpdate.id);
                if (findAddress == null) throw new FailureRequestException(404, "Não existe endereço cadastrado para esse ID");
                _mapper.Map(addressUpdate, findAddress);
                findAddress.updatedAt = DateTime.UtcNow;
                return await _baseRepository.UpdateAsync(findAddress);
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
