using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Stock
{
    public class StockCreateDto
    {
        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public float amount { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public Guid productId { get; set; }
    }
}
