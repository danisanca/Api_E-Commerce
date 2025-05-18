using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class CategoriesModel : BaseEntity
    {
        public string name { get; set; }
        public string? imageUrl { get; set; }

    }
}
