namespace ApiEstoque.Dto.HistoryPurchase
{
    public class HistoryPurchaseDto
    {
        public int id { get; set; }
        public int productId { get; set; }
        public float price { get; set; }
        public int userId { get; set; }
        public float amount { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
