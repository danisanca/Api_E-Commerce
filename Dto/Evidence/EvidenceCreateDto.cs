using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Evidence
{
    public class EvidenceCreateDto
    {
       
        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int productId { get; set; }

        [Required(ErrorMessage = "Descrição é um campo obrigatório.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int userId { get; set; }
    }
}
