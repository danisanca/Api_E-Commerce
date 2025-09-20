using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiEstoque.Constants;

namespace ApiEstoque.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();
        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
