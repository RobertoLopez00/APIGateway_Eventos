using EventosAPI.Models;
using EventosAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly EventoContext _context;

        public EventosController(EventoContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetEventos()
        {
            var eventos = await _context.Eventos
                .Include(e => e.Participantes)
                .Include(e => e.Organizadores)
                .Select(x => new {
                    x.Id,
                    x.Nombre,
                    x.Fecha,
                    x.Lugar,
                    Participantes = x.Participantes.Select(p => new { p.Id, p.Nombre, p.Email }),
                    Organizadores = x.Organizadores.Select(o => new { o.Id, o.Nombre, o.Cargo })
                })
                .ToListAsync();

            return eventos;
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEventos(int id)
        {
            var e = await _context.Eventos
                .Where(x => x.Id == id)
                .Select(x => new {
                    x.Id,
                    x.Nombre,
                    x.Fecha,
                    x.Lugar,
                    Participantes = x.Participantes.Select(p => new { p.Id, p.Nombre, p.Email }),
                    Organizadores = x.Organizadores.Select(o => new { o.Id, o.Nombre, o.Cargo })
                })
                .FirstOrDefaultAsync();

            if (e == null) return NotFound();
            return e;
        }

        // PUT: api/Eventos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] UpdateEventoDto dto)
        {
            // 1) Validación de modelo (JSON + DataAnnotations)
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // 2) Buscar el evento por ID (solo una vez)
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound(new { message = $"No existe un evento con id {id}" });

            // 3) Mapear campos permitidos (evita overposting)
            evento.Nombre = dto.Nombre;
            evento.Fecha = dto.FechaEvento;
            evento.Lugar = dto.Lugar;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Revalidar existencia por si alguien lo borró durante la actualización
                var stillExists = await _context.Eventos.AnyAsync(e => e.Id == id);
                if (!stillExists)
                    return NotFound(new { message = $"El evento {id} fue eliminado durante la actualización" });

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error de concurrencia al actualizar el evento" });
            }

            // Puedes devolver 204 si prefieres
            return Ok(evento);
        }

        // POST: api/Eventos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEventos(Evento eventos)
        {
            _context.Eventos.Add(eventos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventos", new { id = eventos.Id }, eventos);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventos(int id)
        {
            var eventos = await _context.Eventos.FindAsync(id);
            if (eventos == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(eventos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventosExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
