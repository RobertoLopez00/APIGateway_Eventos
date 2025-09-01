using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models
{
    public class Eventos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Lugar { get; set; }
    }
}
