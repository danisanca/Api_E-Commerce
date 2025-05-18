using ApiEstoque.Helpers;
using ApiEstoque.Models.Base;

namespace ApiEstoque.Repository.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<T> SelectByIdAsync(int id);
        Task<IEnumerable<T>> SelectAllByStatusAsync(
            FilterGetRoutes status = FilterGetRoutes.Ativo);
    }
}
/*
 

  public async Task<UserDto> CreateNewUser(UserCreateDto userCreate, TypeUserEnum typeUser, string? TokenAdmin = null)
        {
            try
            {
                UserModel findEmail = await _userRepository.GetUserByEmail(userCreate.email);
                if (findEmail != null) throw new FailureRequestException(409, "E-mail ja cadastrado");
                UserModel findUsername = await _userRepository.GetUserByUsername(userCreate.username);
                if (findUsername != null) throw new FailureRequestException(409, "Username ja cadastrado");
                if (TokenAdmin != null && TokenAdmin != _tokenAdmin) throw new FailureRequestException(401, $"TokenAdmin Invalido");


                var model = _mapper.Map<UserModel>(userCreate);
                model.SetPasswordHash();
                model.status = StandartStatus.Ativo.ToString();
                if (typeUser == TypeUserEnum.Standart) model.typeAccount = TypeUserEnum.Standart.ToString();
                if (typeUser == TypeUserEnum.Admin) model.typeAccount = TypeUserEnum.Admin.ToString();
                if (typeUser == TypeUserEnum.Owner) model.typeAccount = TypeUserEnum.Owner.ToString();

                return _mapper.Map<UserDto>(await _baseRepository.InsertAsync(model));
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
 
 
 */