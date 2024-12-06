using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Product
{
    public class ProductCreateDto
    {
        [StringLength(45, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Preço é um campo obrigatório.")]
        public float price { get; set; }

        [Required(ErrorMessage = "Id da Categoria é um campo obrigatório.")]
        public int categoriesId { get; set; }

        [Required(ErrorMessage = "Id da Loja é um campo obrigatório.")]
        public int shopId { get; set; }

        public int? imageId { get; set; }

        [StringLength(255, ErrorMessage = "Descrição deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Descrição é um campo obrigatório.")]
        public string description { get; set; }
    }
}
