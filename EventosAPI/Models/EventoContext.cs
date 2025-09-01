using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Models
{
    public class EventoContext : DbContext
    {
       public EventoContext(DbContextOptions<EventoContext> options) : base(options)
        {
        }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<Participantes> Participantes { get; set; }
        public DbSet<Organizadores> Organizadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Siembra de datos en las tablas
            modelBuilder.Entity<Eventos>().HasData(
                new Eventos { Id = 1, Nombre = "Conferencia de Tecnología", Fecha = new DateTime(2023, 11, 15), Lugar = "Auditorio Principal" },
                new Eventos { Id = 2, Nombre = "Taller de Desarrollo Web", Fecha = new DateTime(2023, 12, 5), Lugar = "Sala de Conferencias A" }
                );

            modelBuilder.Entity<Participantes>().HasData(
                new Participantes { Id = 1, Nombre = "Juan Pérez", Email = "JuanP@gmail.com", EventoId = 1},
                new Participantes { Id = 2, Nombre = "María Gómez", Email = "MariaG@gmail.com", EventoId = 2 }
                );

            modelBuilder.Entity<Organizadores>().HasData(
                new Organizadores { Id = 1, Nombre = "Carlos López", Cargo = "Coordinador", EventoId = 1 },
                new Organizadores { Id = 2, Nombre = "Ana Martínez", Cargo = "Asistente", EventoId = 2 }
                );
        }
    }
}
