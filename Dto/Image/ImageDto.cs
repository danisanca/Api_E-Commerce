namespace ApiEstoque.Dto.Image
{
    public class ImageDto
    {
        public int id { get; set; }
        public string url { get; set; }
        public int shopId { get; set; }
        public int productId { get; set; }
        public float size { get; set; }
        public string status { get; set; }
    }
}
