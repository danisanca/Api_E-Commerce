using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Image
{
    public class ImageCreateDto
    {

        [Required(ErrorMessage = "Url é um campo obrigatório.")]
        public string url { get; set; }

        [Required(ErrorMessage = "Tamanho da imagem é um campo obrigatório.")]
        public float size { get; set; }

        [Required(ErrorMessage = "ShopId é um campo obrigátorio.")]
        public Guid shopId { get; set; }

        [Required(ErrorMessage = "ProductId é um campo obrigátorio.")]
        public Guid productId { get; set; }

    }
}
