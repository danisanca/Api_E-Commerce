using SharedBase.Models;

namespace OrderAPI.Models
{
    public class OrderDetail : BaseEntity
    {
        public Guid OrderHeaderId { get; set; }
        public virtual OrderHeader OrderHeader { get;set;}
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
