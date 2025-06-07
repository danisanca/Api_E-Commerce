using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class HistoryMovimentModel : BaseEntity
    {
        public Guid productId { get; set; }
        public virtual ProductModel product { get; set; }
        public string userId { get; set; }
        public virtual UserModel user { get; set; }
        public float amount { get; set; }
        public string action { get; set; }
    }
}
