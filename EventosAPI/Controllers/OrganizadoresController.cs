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
    public class OrganizadoresController : ControllerBase
    {
        private readonly EventoContext _context;

        public OrganizadoresController(EventoContext context)
        {
            _context = context;
        }

        // GET: api/Organizadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizador>>> GetOrganizadores()
        {
            return await _context.Organizadores.ToListAsync();
        }

        // GET: api/Organizadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organizador>> GetOrganizadores(int id)
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
        public async Task<IActionResult> PutOrganizador(int id, [FromBody] UpdateOrganizadorDto dto)
        {
            var organizador = await _context.Organizadores.FindAsync(id);
            if (organizador == null)
            {
                return NotFound(new { message = $"No existe un organizador con id {id}" });
            }

            organizador.Nombre = dto.Nombre;
            organizador.Cargo = dto.Cargo;
            organizador.EventoId = dto.EventoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error de concurrencia al actualizar el organizador" });
            }

            return Ok(organizador); // o NoContent() si prefieres no devolver nada
        }


        // POST: api/Organizadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Organizador>> PostOrganizadores(Organizador organizadores)
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
