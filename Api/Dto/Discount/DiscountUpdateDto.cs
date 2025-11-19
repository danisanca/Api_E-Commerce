using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Discount
{
    public class DiscountUpdateDto
    {
        [Required(ErrorMessage = "Id do desconto é um campo obrigatório.")]
        public Guid id { get; set; }

        [Required(ErrorMessage = "Valor do desconto é um campo obrigatório.")]
        public float percentDiscount { get; set; }

    }
}
