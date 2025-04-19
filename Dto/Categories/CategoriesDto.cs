using ApiEstoque.Models;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string status { get; set; }
    }
}
