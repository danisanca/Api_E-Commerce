namespace ApiEstoque.Dto.Shop
{
    public class ShopDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int userId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
