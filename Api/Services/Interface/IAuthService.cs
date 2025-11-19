using ApiEstoque.Dto.Login;

namespace ApiEstoque.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginUserDto user);
        Task<LoginResponse> RefreshToken(RefreshTokenDto data);
    }
}
