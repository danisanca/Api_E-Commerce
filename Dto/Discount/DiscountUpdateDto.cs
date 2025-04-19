using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Discount
{
    public class DiscountUpdateDto
    {
        [Required(ErrorMessage = "Id do desconto é um campo obrigatório.")]
        public int productId { get; set; }
        public string? description { get; set; }
        public float? percentDiscount { get; set; }

    }
}
