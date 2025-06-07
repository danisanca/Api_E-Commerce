using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Discount
{
    public class DiscountCreateDto
    {
        [Required(ErrorMessage = "Descrição do desconto é um campo obrigatório.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Percentual de Desconto é um campo obrigatório.")]
        public float percentDiscount { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public Guid productId { get; set; }
    }
}
