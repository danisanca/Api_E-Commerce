using ApiEstoque.Constants;
using SharedBase.Models;

namespace ApiEstoque.Models
{
    public class CategoriesModel : BaseEntity
    {
        public string name { get; set; }
        public string? imageUrl { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();

    }
}
