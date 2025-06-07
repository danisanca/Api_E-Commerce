namespace ApiEstoque.Dto.Evidence
{
    public class EvidenceDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string description { get; set; }
        public string productName { get; set; }
        public DateTime createdAt { get; set; }
    }
}
