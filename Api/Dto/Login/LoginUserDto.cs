using ApiEstoque.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Login
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Necessário informar um email.")]
        [EmailAddress(ErrorMessage = "Formato do email invalido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        public string password { get; set; }

       
    }
}
