namespace ApiEstoque.Dto.Stock
{
    public class StockDto
    {
        public int id { get; set; }
        public float amount { get; set; }
        public string status { get; set; }
        public int productId { get; set; }
    }
}
