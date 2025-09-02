using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dto.Image
{
    public class ImageCreateDto
    {
        public List<IFormFile>? images { get; set; }

        [Required(ErrorMessage = "ShopId é um campo obrigátorio.")]
        public Guid shopId { get; set; }

        [Required(ErrorMessage = "ProductId é um campo obrigátorio.")]
        public Guid productId { get; set; }

    }
}
