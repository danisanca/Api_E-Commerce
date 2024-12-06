using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Shop
{
    public class ShopUpdateDto
    {
        [Required(ErrorMessage = "Id do shop é um campo obrigatório.")]
        public int shopId { get; set; }

        [Required(ErrorMessage = "Nome da loja é um campo obrigatório.")]
        public string name { get; set; }
    }
}
