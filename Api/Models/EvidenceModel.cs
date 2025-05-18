using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class EvidenceModel : BaseEntity
    {
        public int productId { get; set; }
        public virtual ProductModel product { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }
        public string description { get; set; }
    }
}
