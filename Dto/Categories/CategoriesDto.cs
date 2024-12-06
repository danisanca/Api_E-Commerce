using ApiEstoque.Models;

namespace ApiEstoque.Dto.Categories
{
    public class CategoriesDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int shopId { get; set; }
        public string status { get; set; }
    }
}
