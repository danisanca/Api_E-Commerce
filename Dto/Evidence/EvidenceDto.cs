namespace ApiEstoque.Dto.Evidence
{
    public class EvidenceDto
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public int productId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
