using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesUpdateDto
    {
        [Required(ErrorMessage = "id da categoria é um campo obrigatório.")]
        public int idCategories { get; set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Id da loja é um campo obrigatório.")]
        public int shopId { get; set; }
    }
}
