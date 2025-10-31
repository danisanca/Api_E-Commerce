using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SharedBase.Models;

namespace CartAPI.Models
{
    public class CartDetail: BaseEntity
    {
        public Guid CartHeaderId { get; set; }
        public virtual CartHeader CartHeader { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
