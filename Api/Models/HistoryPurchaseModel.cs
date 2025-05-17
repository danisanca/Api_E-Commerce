namespace ApiEstoque.Models
{
    public class HistoryPurchaseModel
    {
        public int id { get; set; }
        public string cartProducts { get; set; }
        public float price { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public string externalReference { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
