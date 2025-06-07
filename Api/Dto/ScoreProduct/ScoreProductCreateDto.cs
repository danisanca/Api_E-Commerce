using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.ScoreProduct
{
    public class ScoreProductCreateDto
    {
        [Required(ErrorMessage = "Nota é um campo obrigatório.")]
        public float amountStars { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public Guid productId { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public string userId { get; set; }

    }
}
