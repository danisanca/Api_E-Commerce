using ApiEstoque.Dto.Adress;
using ApiEstoque.Models;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserModel> _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IBaseRepository<AddressModel> _baseRepository;

        public AddressService(IMapper mapper,
            IBaseRepository<UserModel> userRepository, 
            IAddressRepository addressRepository, 
            IBaseRepository<AddressModel> baseRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _baseRepository = baseRepository;
        }

        public async Task<AddressDto> CreateAddress(AddressCreateDto addressCreate)
        {
            try
            {
                var findUser = await _userRepository.SelectByIdAsync(addressCreate.userId);
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

        public async Task<AddressDto> GetAddressById(int id)
        {
            try
            {
                var findAddress = await _baseRepository.SelectByIdAsync(id);
                if (findAddress == null) throw new FailureRequestException(404, "Não existe endereço cadastrado para esse ID");
                return _mapper.Map<AddressDto>(findAddress);
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

        public async Task<AddressDto> GetAddressByUserId(int userId)
        {
            try
            {
                var findUser = await _userRepository.SelectByIdAsync(userId);
                if (findUser == null) throw new FailureRequestException(404, "Não existe usuario com esse id");
                var findAddress = await _addressRepository.GetAddressByUserId(userId);
                if (findAddress == null) throw new FailureRequestException(404, "Não existe endereço cadastrado para esse ID");
                return _mapper.Map<AddressDto>(findAddress);
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

        public async Task<bool> UpdateAddressById(AddressUpdateDto addressUpdate)
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
