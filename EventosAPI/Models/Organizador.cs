using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventosAPI.Models
{
    public class Organizador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Cargo { get; set; }

        [Required]
        [ForeignKey("Evento")]
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
