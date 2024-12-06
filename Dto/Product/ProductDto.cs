namespace ApiEstoque.Dto.Product
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public int categoriesId { get; set; }
        public int shopId { get; set; }
        public int? imageId { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
