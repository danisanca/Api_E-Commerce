using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CartAPI.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
