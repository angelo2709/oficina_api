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
    public class FornecedoresPecasController : ControllerBase
    {
        private readonly OficinaContext _context;

        public FornecedoresPecasController(OficinaContext context)
        {
            _context = context;
        }

        // GET: api/FornecedoresPecas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorPeca>>> GetFornecedoresPecas()
        {
            return await _context.FornecedoresPecas.ToListAsync();
        }

        // GET: api/FornecedoresPecas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorPeca>> GetFornecedorPeca(int id)
        {
            var fornecedorPeca = await _context.FornecedoresPecas.FindAsync(id);

            if (fornecedorPeca == null)
            {
                return NotFound();
            }

            return fornecedorPeca;
        }

        // PUT: api/FornecedoresPecas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornecedorPeca(int id, FornecedorPeca fornecedorPeca)
        {
            if (id != fornecedorPeca.FornecedorPecaId)
            {
                return BadRequest();
            }

            _context.Entry(fornecedorPeca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorPecaExists(id))
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

        // POST: api/FornecedoresPecas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FornecedorPeca>> PostFornecedorPeca(FornecedorPeca fornecedorPeca)
        {
            _context.FornecedoresPecas.Add(fornecedorPeca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornecedorPeca", new { id = fornecedorPeca.FornecedorPecaId }, fornecedorPeca);
        }

        // DELETE: api/FornecedoresPecas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedorPeca(int id)
        {
            var fornecedorPeca = await _context.FornecedoresPecas.FindAsync(id);
            if (fornecedorPeca == null)
            {
                return NotFound();
            }

            _context.FornecedoresPecas.Remove(fornecedorPeca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FornecedorPecaExists(int id)
        {
            return _context.FornecedoresPecas.Any(e => e.FornecedorPecaId == id);
        }
    }
}
