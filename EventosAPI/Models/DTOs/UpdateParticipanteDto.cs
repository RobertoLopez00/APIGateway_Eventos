using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models.DTOs
{
    public class UpdateParticipanteDto
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; } = null!;

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = null!;

        // FK al evento asociado
        [Required]
        public int EventoId { get; set; }
    }
}
