using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
    }
}
