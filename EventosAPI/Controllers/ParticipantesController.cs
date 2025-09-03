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
    public class ParticipantesController : ControllerBase
    {
        private readonly EventoContext _context;

        public ParticipantesController(EventoContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipantes(int id)
        {
            var participantes = await _context.Participantes.FindAsync(id);

            if (participantes == null)
            {
                return NotFound();
            }

            return participantes;
        }

        // PUT: api/Participantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutParticipante(int id, [FromBody] UpdateParticipanteDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
                return NotFound(new { message = $"No existe un participante con id {id}" });

            var eventoExiste = await _context.Eventos.AnyAsync(e => e.Id == dto.EventoId);
            if (!eventoExiste)
                return UnprocessableEntity(new { message = $"No existe un evento con id {dto.EventoId}" });

            participante.Nombre = dto.Nombre;
            participante.Email = dto.Email;
            participante.EventoId = dto.EventoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var stillExists = await _context.Participantes.AnyAsync(p => p.Id == id);
                if (!stillExists)
                    return NotFound(new { message = $"El participante {id} fue eliminado durante la actualización" });

                // Otra causa de concurrencia
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error de concurrencia al actualizar el participante" });
            }

            return Ok(participante);
        }

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipantes(Participante participantes)
        {
            _context.Participantes.Add(participantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipantes", new { id = participantes.Id }, participantes);
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantes(int id)
        {
            var participantes = await _context.Participantes.FindAsync(id);
            if (participantes == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participantes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantesExists(int id)
        {
            return _context.Participantes.Any(e => e.Id == id);
        }
    }
}
