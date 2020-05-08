using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploEF.Models;
using ExemploEF.DTO;

namespace ExemploEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DisciplinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplinas>>> GetDisciplinas()
        {
            return await _context.Disciplinas.Include(p => p.Professor).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplinas>> GetDisciplinas(int id)
        {
            var disciplinas = await _context.Disciplinas.Where(disc => disc.Id == id)
                .Select(dados => new 
                {
                    Disciplina = dados.Nome,
                    Professor = dados.Professor.Nome
                }).FirstAsync();

            if (disciplinas == null)
            {
                return NotFound();
            }

            return Ok(disciplinas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplinas(int id, DisciplinaDTO disciplinaDTO)
        {
            var disciplinas = await _context.Disciplinas.FindAsync(id);
            if (disciplinas == null)
            {
                return NotFound();
            }
            else if (!_context.Professor.Any(prof => prof.Id == disciplinaDTO.Professor))
            {
                return NotFound();
            }

            disciplinas.Nome = disciplinaDTO.Nome;
            disciplinas.Professor = await _context.Professor.FindAsync(disciplinaDTO.Professor);

            _context.Entry(disciplinas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DisciplinasExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DisciplinaDTO>> PostDisciplinas(DisciplinaDTO disciplinaDTO)
        {
            if (!_context.Professor.Any(prof => prof.Id == disciplinaDTO.Professor))
            {
                return NotFound();
            }

            var disciplinas = new Disciplinas
            {
                Nome = disciplinaDTO.Nome,
                Professor = await _context.Professor.FindAsync(disciplinaDTO.Professor)
            };

            _context.Disciplinas.Add(disciplinas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisciplinas", new { id = disciplinas.Id }, disciplinas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Disciplinas>> DeleteDisciplinas(int id)
        {
            var disciplinas = await _context.Disciplinas
                .Include(prof => prof.Professor)
                .Where(disc => disc.Id == id)
                .FirstAsync();
            if (disciplinas == null)
            {
                return NotFound();
            }

            _context.Disciplinas.Remove(disciplinas);
            await _context.SaveChangesAsync();

            return disciplinas;
        }

        private bool DisciplinasExists(int id)
        {
            return _context.Disciplinas.Any(e => e.Id == id);
        }
    }
}
