using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.HistoryMoviment
{
    public class HistoryMovimentCreateDto
    {
        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public Guid productId { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public float amount { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public string userId { get; set; }

        [Required(ErrorMessage = "Tipo da Movimentação é um campo obrigatório.")]
        public string action { get; set; }
    }
}
