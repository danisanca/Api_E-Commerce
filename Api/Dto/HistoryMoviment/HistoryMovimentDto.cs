namespace ApiEstoque.Dto.HistoryMoviment
{
    public class HistoryMovimentDto
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int userId { get; set; }
        public float amount { get; set; }
        public string action { get; set; }
        public DateTime createdAt { get; set; }
    }
}
