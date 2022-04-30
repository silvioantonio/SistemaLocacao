using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("Cliente")]
    public class Client : BaseEntity
    {
        [Required]
        [StringLength(200)]
        [Column("nome")]
        public string Name { get; set; }

        [Required]
        [StringLength(11)]
        [Column("cpf")]
        public string CPF { get; set; }

        [Required]
        [Column("data_nascimento")]
        public DateTime BirthDate { get; set; }
    }
}
