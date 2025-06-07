namespace ApiEstoque.Dto.Product
{
    public class ProductDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public Guid categoriesId { get; set; }
        public Guid shopId { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
