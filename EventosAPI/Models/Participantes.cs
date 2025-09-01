using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models
{
    public class Participantes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int EventoId { get; set; }
        public Eventos Evento { get; set; }
    }
}
