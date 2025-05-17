namespace ApiEstoque.Models
{
    public class EvidenceModel
    {
        public int id { get; set; }
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
