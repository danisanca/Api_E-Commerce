using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class OrderDetailModel : BaseEntity
    {
        public Guid orderHeaderId { get; set; }
        public virtual OrderHeaderModel orderHeader { get; set; }
        public Guid productId { get; set; }
        public virtual ProductModel products { get; set; }
        public int amount { get; set; }
        public float price { get; set; }
        

    }
}
