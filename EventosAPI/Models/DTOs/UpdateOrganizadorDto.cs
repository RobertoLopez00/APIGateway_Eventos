namespace EventosAPI.Models.DTOs
{
    public class UpdateOrganizadorDto
    {
        public string Nombre { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public int EventoId { get; set; }
    }
}
