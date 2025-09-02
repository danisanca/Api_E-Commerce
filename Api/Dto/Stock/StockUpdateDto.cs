using System.ComponentModel.DataAnnotations;
using ApiEstoque.Constants;

namespace ApiEstoque.Dto.Stock
{
    public class StockUpdateDto
    {
      
        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public float amount { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public Guid productId { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public string userId { get; set; }

        [Required(ErrorMessage = "Id do shop é um campo obrigatório.")]
        public string shopId { get; set; }

        [Required(ErrorMessage = "Ação é um campo obrigatório.")]
        public string action { get; set; }
    }
}
