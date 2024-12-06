using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesCreateDto
    {
        [Required(ErrorMessage = "Nome da categoria é um campo obrigatório.")]
        public string name { get; set; }

        [Required(ErrorMessage = "ShopId é um campo obrigátorio.")]
        public int shopId { get; set; }
    }
}
