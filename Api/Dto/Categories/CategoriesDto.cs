using ApiEstoque.Models;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string status { get; set; }
    }
}
