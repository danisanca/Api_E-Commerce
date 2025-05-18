using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class HistoryPurchaseModel : BaseEntity
    {
        public string cartProducts { get; set; }
        public float price { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public string externalReference { get; set; }
    }
}
