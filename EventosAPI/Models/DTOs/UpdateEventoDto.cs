using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models.DTOs
{
    public class UpdateEventoDto
    {
        [Required, StringLength(100, MinimumLength = 5)]
        public string Nombre { get; set; } = null!;

        [Required] // espera ISO 8601 en JSON, p. ej. "2025-09-15T15:00:00"
        public DateTime FechaEvento { get; set; }

        [Required, StringLength(100, MinimumLength = 5)]
        public string Lugar { get; set; } = null!;
    }
}
