using ApiEstoque.Dto.Login;

namespace ApiEstoque.Services.Interface
{
    public interface ILoginService
    {
        Task<object> Login(LoginDto user);
    }
}
