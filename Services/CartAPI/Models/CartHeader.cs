using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CartAPI.Models
{
    public class CartHeader
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
