using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models
{
    public class Organizadores
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
        public int EventoId { get; set; }
        public Eventos Evento { get; set; }
    }
}
