using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosAPI.Models;

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
        public async Task<ActionResult<IEnumerable<Participantes>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participantes>> GetParticipantes(int id)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipantes(int id, Participantes participantes)
        {
            if (id != participantes.Id)
            {
                return BadRequest();
            }

            _context.Entry(participantes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participantes>> PostParticipantes(Participantes participantes)
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
