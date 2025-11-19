using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "id do usuario é um campo obrigatório.")]
        public string userId { get; set; }

        [Required(ErrorMessage = "Senha atual é um campo obrigatório.")]
        public string currentPassword { get; set; }

        [Required(ErrorMessage = "Nova senha é um campo obrigatório.")]
        public string newPassword { get; set; }
    }
}
