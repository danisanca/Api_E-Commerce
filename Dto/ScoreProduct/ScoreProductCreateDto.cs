using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.ScoreProduct
{
    public class ScoreProductCreateDto
    {
        [Required(ErrorMessage = "Nota é um campo obrigatório.")]
        public float amountStars { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int productId { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int userId { get; set; }

    }
}
