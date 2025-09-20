using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CartAPI.Models.Base;

namespace CartAPI.Models
{
    public class CartHeader : BaseEntity
    {
        public string UserId { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }
}
