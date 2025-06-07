using System.ComponentModel.DataAnnotations;
using ApiEstoque.Dto.Adress;

namespace ApiEstoque.Dto.User
{
    public class UserCreateDto
    {
        [StringLength(45, ErrorMessage = "Primeiro nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Primeiro nome é um campo obrigatório.")]
        public string FirstName { get; set; }

        [StringLength(45, ErrorMessage = "Sobrenome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Sobrenome é um campo obrigatório.")]
        public string LastName { get; set; }

        [StringLength(45, ErrorMessage = "Username deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Username é um campo obrigatório.")]
        public string Username { get; set; }

        [MinLength(4, ErrorMessage = "Senha deve ter no minimo {1} characters.")]
        [StringLength(24, ErrorMessage = "Senha deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} characters.")]
        [EmailAddress(ErrorMessage = "Formato do email é invalido.")]
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Endereço é um campo obrigatório.")]
        public AddressCreateDto AddressCreateDto { get; set; }

    }
}
