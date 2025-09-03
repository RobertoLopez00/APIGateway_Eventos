using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Models
{
    public class EventoContext : DbContext
    {
       public EventoContext(DbContextOptions<EventoContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Siembra de datos en las tablas
            modelBuilder.Entity<Evento>().HasData(
                new Evento { Id = 1, Nombre = "Conferencia de Tecnología", Fecha = new DateTime(2023, 11, 15), Lugar = "Auditorio Principal" },
                new Evento { Id = 2, Nombre = "Taller de Desarrollo Web", Fecha = new DateTime(2023, 12, 5), Lugar = "Sala de Conferencias A" }
                );

            modelBuilder.Entity<Participante>().HasData(
                new Participante { Id = 1, Nombre = "Juan Pérez", Email = "JuanP@gmail.com", EventoId = 1},
                new Participante { Id = 2, Nombre = "María Gómez", Email = "MariaG@gmail.com", EventoId = 2 }
                );

            modelBuilder.Entity<Organizador>().HasData(
                new Organizador { Id = 1, Nombre = "Carlos López", Cargo = "Coordinador", EventoId = 1 },
                new Organizador { Id = 2, Nombre = "Ana Martínez", Cargo = "Asistente", EventoId = 2 }
                );
        }
    }
}
