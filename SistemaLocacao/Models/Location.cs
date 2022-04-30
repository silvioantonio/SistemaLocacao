using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaLocacao.Models
{
    [Table("Locacao")]
    public class Location : BaseEntity
    {
        [Column("data_locacao")]
        public DateTime LeaseDate{ get; set; }

        [Column("data_devolucao")]
        public DateTime ReturnDate { get; set; }

        [Required]
        [Column("id_cliente")]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }

        [Required]
        [Column("id_filme")]
        public int MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie? Movie { get; set; }
    }
}
