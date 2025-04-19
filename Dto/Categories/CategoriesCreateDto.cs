using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesCreateDto
    {
        [Required(ErrorMessage = "Nome da categoria é um campo obrigatório.")]
        public string name { get; set; }

        public string? imageUrl { get; set; }
    }
}
