using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CartAPI.Models
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public virtual CartHeader CartHeader { get; set; }
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
