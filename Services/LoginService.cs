using ApiEstoque.Dto.Login;
using ApiEstoque.Models;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using ApiEstoque.Services.Security;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ApiEstoque.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;
        private SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository userRepository, IShopRepository shopRepository, IMapper mapper,
            SigningConfigurations signingConfigurations, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _shopRepository = shopRepository;
            _mapper = mapper;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }

      


        public async Task<object> Login(LoginDto user)
        {
            try
            {
                var baseUser = new UserModel();
                if (user != null && !string.IsNullOrWhiteSpace(user.email) && !string.IsNullOrWhiteSpace(user.password))
                {
                    baseUser = _mapper.Map<UserModel>(await _userRepository.GetUserByEmail(user.email));
                    if (baseUser == null) { 
                        return new { authenticated = false, message = "Login Incorreto." };
                    }
                    else
                    {
                        user.SetPasswordHash();
                        if (baseUser.password != user.password) return new { authenticated = false, message = "Senha Incorreta." };

                        var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.email),
                        new[]{
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier,$"{baseUser.id}"),
                        }
                        );
                        DateTime createDate = DateTime.Now;
                        DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(_configuration["Seconds"]));
                        var handler = new JwtSecurityTokenHandler();
                        string token = CreateToken(identity, createDate, expirationDate, handler);
                        var findShop = await _shopRepository.GetShopByUserId(baseUser.id);
                        var idShop = findShop == null? 0 : findShop.id;
                        var json = SuccessObject(createDate, expirationDate, token);
                        return json;
                       
                    }

                }
                else
                {
                    throw new FailureRequestException(409, "Verifique as credenciais inseridas.");
                }
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

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"],
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = "Bearer " + token,
                message = "Usuário logado com sucesso."
            };
        }
    }
}
