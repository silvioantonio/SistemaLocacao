using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("Filme")]
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("titulo")]
        public string Title { get; set; }

        [Required]
        [Column("classificacao_indicativa")]
        public int ParentalRating { get; set; }

        [Required]
        [Column("lancamento")]
        public byte Lounch { get; set; }
    }
}
