using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Image
{
    public class ImageUpdateDto
    {
        [Required(ErrorMessage = "Url é um campo obrigatório.")]
        public Guid idImage { get; set; }

        [Required(ErrorMessage = "Url é um campo obrigatório.")]
        public string url { get; set; }

        [Required(ErrorMessage = "Tamanho da imagem é um campo obrigatório.")]
        public float size { get; set; }
    }
}
