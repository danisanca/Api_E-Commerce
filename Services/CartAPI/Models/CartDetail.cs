using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SharedBase.Models;

namespace CartAPI.Models
{
    public class CartDetail: BaseEntity
    {
        public Guid CartHeaderId { get; set; }
        public virtual CartHeader CartHeader { get; set; }
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
