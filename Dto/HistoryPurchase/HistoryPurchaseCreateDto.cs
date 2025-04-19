using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.HistoryPurchase
{
    public class HistoryPurchaseCreateDto
    {
        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int productId { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public float amount { get; set; }

        [Required(ErrorMessage = "Preço é um campo obrigatório.")]
        public float price { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int userId { get; set; }
    }
}
