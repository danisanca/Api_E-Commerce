using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Shop
{
    public class ShopCreateDto
    {
        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int userId { get; set; }

        [Required(ErrorMessage = "Nome da loja é um campo obrigatório.")]
        public string name { get; set; }

    }
}
