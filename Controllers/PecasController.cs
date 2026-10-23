using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oficina_api.Data;
using oficina_api.Models;

namespace oficina_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private readonly OficinaContext _context;

        public PecasController(OficinaContext context)
        {
            _context = context;
        }

        // GET: api/Pecas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peca>>> GetPecas()
        {
            return await _context.Pecas.ToListAsync();
        }

        // GET: api/Pecas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peca>> GetPeca(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);

            if (peca == null)
            {
                return NotFound();
            }

            return peca;
        }

        // PUT: api/Pecas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeca(int id, Peca peca)
        {
            if (id != peca.PecaId)
            {
                return BadRequest();
            }

            _context.Entry(peca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PecaExists(id))
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

        // POST: api/Pecas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Peca>> PostPeca(Peca peca)
        {
            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeca", new { id = peca.PecaId }, peca);
        }

        // DELETE: api/Pecas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeca(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);
            if (peca == null)
            {
                return NotFound();
            }

            _context.Pecas.Remove(peca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PecaExists(int id)
        {
            return _context.Pecas.Any(e => e.PecaId == id);
        }
    }
}
