namespace ApiEstoque.Dto.Image
{
    public class ImageDto
    {
        public Guid id { get; set; }
        public string url { get; set; }
        public Guid shopId { get; set; }
        public Guid productId { get; set; }
        public float size { get; set; }
        public string status { get; set; }
    }
}
