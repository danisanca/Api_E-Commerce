using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.User
{
    public class UserCreateDto
    {
        [StringLength(45, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string name { get; set; }

        [StringLength(45, ErrorMessage = "Username deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Username é um campo obrigatório.")]
        public string username { get; set; }

        [MinLength(4, ErrorMessage = "Senha deve ter no minimo {1} characters.")]
        [StringLength(24, ErrorMessage = "Senha deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        public string password { get; set; }

        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} characters.")]
        [EmailAddress(ErrorMessage = "Formato do email é invalido.")]
        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        public string email { get; set; }

    }
}
