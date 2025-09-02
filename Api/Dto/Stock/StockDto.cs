namespace ApiEstoque.Dto.Stock
{
    public class StockDto
    {
        public Guid id { get; set; }
        public float amount { get; set; }
        public string status { get; set; }
        public Guid productId { get; set; }
        public Guid shopId { get; set; }
    }
}
