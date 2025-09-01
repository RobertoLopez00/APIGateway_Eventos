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
    public class OrganizadoresController : ControllerBase
    {
        private readonly EventoContext _context;

        public OrganizadoresController(EventoContext context)
        {
            _context = context;
        }

        // GET: api/Organizadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizadores>>> GetOrganizadores()
        {
            return await _context.Organizadores.ToListAsync();
        }

        // GET: api/Organizadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizadores>> GetOrganizadores(int id)
        {
            var organizadores = await _context.Organizadores.FindAsync(id);

            if (organizadores == null)
            {
                return NotFound();
            }

            return organizadores;
        }

        // PUT: api/Organizadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizadores(int id, Organizadores organizadores)
        {
            if (id != organizadores.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizadoresExists(id))
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

        // POST: api/Organizadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organizadores>> PostOrganizadores(Organizadores organizadores)
        {
            _context.Organizadores.Add(organizadores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizadores", new { id = organizadores.Id }, organizadores);
        }

        // DELETE: api/Organizadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizadores(int id)
        {
            var organizadores = await _context.Organizadores.FindAsync(id);
            if (organizadores == null)
            {
                return NotFound();
            }

            _context.Organizadores.Remove(organizadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizadoresExists(int id)
        {
            return _context.Organizadores.Any(e => e.Id == id);
        }
    }
}
