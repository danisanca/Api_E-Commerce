namespace ApiEstoque.Dto.Shop
{
    public class ShopDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string userId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
