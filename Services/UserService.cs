using ApiEstoque.Dto.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using ApiEstoque.Helpers;

namespace ApiEstoque.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly string _tokenAdmin;
        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenAdmin = configuration["Tokens:TokenAdmin"]; ;
        }

        public async Task<UserDto> CreateUser(UserCreateDto userCreate, TypeUserEnum typeUser, string? TokenAdmin = null)
        {
            try
            {
                UserModel findEmail = await _userRepository.GetUserByEmail(userCreate.email);
                if (findEmail != null) throw new FailureRequestException(409,"E-mail ja cadastrado");
                UserModel findUsername = await _userRepository.GetUserByUsername(userCreate.username);
                if (findUsername != null) throw new FailureRequestException(409,"Username ja cadastrado");
                if (TokenAdmin != null && TokenAdmin != _tokenAdmin) throw new FailureRequestException(401,$"TokenAdmin Invalido");


                var model = _mapper.Map<UserModel>(userCreate);
                model.status = StandartStatus.Ativo.ToString();
                if (typeUser == TypeUserEnum.Standart) model.typeAccout = TypeUserEnum.Standart.ToString();
                if (typeUser == TypeUserEnum.Admin) model.typeAccout = TypeUserEnum.Admin.ToString();
                if (typeUser == TypeUserEnum.Owner) model.typeAccout = TypeUserEnum.Owner.ToString();
                //Falta adicionar função para encryptar a senha
                await _userRepository.AddUser(model);
                return _mapper.Map<UserDto>(model);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar usuario. Detalhe do erro: {e.Message}");
            }
        }

        public async Task<List<UserDto>> GetAllUsers(FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                var findUsers = await _userRepository.GetAllUsers(status);
                if (findUsers == null) throw new FailureRequestException(200, "Nenhum usuario cadastrado");
                return _mapper.Map<List<UserDto>>(findUsers);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o usuario. Detalhe do erro:{e.Message}");
            }
        }

       

        public async Task<UserDto> GetUserByEmail(string email)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserByEmail(email);
                if (findUser == null) throw new FailureRequestException(404, "E-mail não cadastrado");
                return _mapper.Map<UserDto>(findUser);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o usuario. Detalhe do erro:{e.Message}");
            }
        }

        public async Task<UserDto> GetUserById(int idUser)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserById(idUser);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario não localizado");
                return _mapper.Map<UserDto>(findUser);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o usuario. Detalhe do erro:{e.Message}");
            }
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserByUsername(username);
                if (findUser == null) throw new FailureRequestException(404, "Username não localizado");
                return _mapper.Map<UserDto>(findUser);
            }

            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o usuario. Detalhe do erro:{e.Message}");
            }
        }

        public async Task<bool> UpdateUser(UserUpdateDto userUpdate)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserById(userUpdate.idUser);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario não localizado");
                if (findUser.email != userUpdate.email)
                {
                    UserModel findEmail = await _userRepository.GetUserByEmail(userUpdate.email);
                    if (findEmail != null) throw new FailureRequestException(409, "E-mail ja cadastro");
                    findUser.email = userUpdate.email;
                }
                    findUser.name = userUpdate.name;
                //Falta adicionar função para encryptar a senha
                await _userRepository.UpdateUser(findUser);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao atualizar o usuario. Detalhe do erro:{e.Message}");
            }
        }

        public async Task<bool> ActiveUser(int idUser)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserById(idUser);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario não localizado");
                if (findUser.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Usuario ja esta ativo");
                findUser.status = StandartStatus.Ativo.ToString();
                await _userRepository.UpdateUser(findUser);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao ativar o usuario. Detalhe do erro:{e.Message}");
            }
        }

        public async Task<bool> DisableUser(int idUser)
        {
            try
            {
                UserModel findUser = await _userRepository.GetUserById(idUser);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario não localizado");
                if (findUser.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Usuario ja esta Desabilitado");
                findUser.status = StandartStatus.Desabilitado.ToString();
                await _userRepository.UpdateUser(findUser);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao desabilitar o usuario. Detalhe do erro:{e.Message}");
            }
        }


    }
}
