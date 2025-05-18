using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class HistoryMovimentModel : BaseEntity
    {
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public float amount { get; set; }
        public string action { get; set; }
    }
}
