namespace ApiEstoque.Dto.HistoryPurchase
{
    public class HistoryPurchaseFullDto
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public ItemDto item { get; set; }
    }
    public class ItemDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public float amount { get; set; }
        public List<string> imageUrl { get; set; }
        public string description { get; set; }
        public float priceProduct { get; set; }
        public float priceTotal { get; set; }
    }
}
