using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Adress
{
    public class AddressCreateDto
    {
        [Required(ErrorMessage = "Endereço é um campo obrigatório.")]
        public string street { get; set; }

        public string complement { get; set; }

        [Required(ErrorMessage = "Bairro é um campo obrigatório.")]
        public string neighborhood { get; set; }

        [Required(ErrorMessage = "Cidade é um campo obrigatório.")]
        public string city { get; set; }

        [StringLength(2, ErrorMessage = "Estado deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Estado é um campo obrigatório.")]
        public string state { get; set; }

        [StringLength(8, ErrorMessage = "Cep deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Cep é um campo obrigatório.")]
        public string zipcode { get; set; }

        [StringLength(11, ErrorMessage = "O telefone deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Telefone é um campo obrigatório.")]
        public string cellPhone { get; set; }

        public string? userId { get; set; } = "";
    }
}
