namespace ApiEstoque.Dto.HistoryMoviment
{
    public class HistoryMovimentDto
    {
        public Guid id { get; set; }
        public Guid productId { get; set; }
        public string userId { get; set; }
        public float amount { get; set; }
        public string action { get; set; }
        public DateTime createdAt { get; set; }
    }
}
