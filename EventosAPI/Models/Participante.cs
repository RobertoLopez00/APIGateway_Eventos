using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventosAPI.Models
{
    public class Participante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        [JsonIgnore]
        public Evento? Evento { get; set; }
    }
}
