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
    public class Aluno_DisciplinaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Aluno_DisciplinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno_Disciplina>>> GetAluno_Disciplina()
        {
            //retorno com include (Retornando todos os dados das tabelas)
            // return await _context.Aluno_Disciplina
            //     .Include(d => d.Disciplina)
            //     .Include(a => a.Aluno)
            //     .Include(p => p.Disciplina.Professor)
            //     .ToListAsync();

            //retorno con Join (Retornando apenas o RA do aluno, o nome da disciplina e o nome do professor)
            return Ok(await _context.Aluno
                    .Join(_context.Aluno_Disciplina, PK => PK.RA, FK => FK.Aluno.RA, (PK,FK) => new 
                    {
                        PK.RA,
                        Disciplina = FK.Disciplina.Nome,
                        Professor = FK.Disciplina.Professor.Nome
                    }).ToListAsync());
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno_Disciplina(int id, Aluno_DisciplinaDTO aluno_DisciplinaDTO)
        {
            var aluno_Disciplina = await _context.Aluno_Disciplina.FindAsync(id);
            if (aluno_Disciplina == null)
            {
                return NotFound();
            }
            else if (!_context.Aluno.Any(x => x.RA == aluno_DisciplinaDTO.Aluno))
            {
                return NotFound();
            }
            else if (!_context.Disciplinas.Any(x => x.Id == aluno_DisciplinaDTO.Disciplina))
            {
                return NotFound();
            }

            aluno_Disciplina.Aluno = _context.Aluno.Find(aluno_DisciplinaDTO.Aluno);
            aluno_Disciplina.Disciplina = _context.Disciplinas.Find(aluno_DisciplinaDTO.Disciplina);

            _context.Entry(aluno_Disciplina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!Aluno_DisciplinaExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<Aluno_DisciplinaDTO>> PostAluno_Disciplina(Aluno_DisciplinaDTO aluno_DisciplinaDTO)
        {
            if (!_context.Aluno.Any(x => x.RA == aluno_DisciplinaDTO.Aluno))
            {
                return NotFound();
            }
            else if (!_context.Disciplinas.Any(x => x.Id == aluno_DisciplinaDTO.Disciplina))
            {
                return NotFound();
            }

            var aluno_Disciplina = new Aluno_Disciplina
            {
                Disciplina = _context.Disciplinas
                        .Include(p => p.Professor)
                        .Where(d => d.Id == aluno_DisciplinaDTO.Disciplina)
                        .First(),
                Aluno = _context.Aluno.Find(aluno_DisciplinaDTO.Aluno)
            };

            _context.Aluno_Disciplina.Add(aluno_Disciplina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno_Disciplina", new { id = aluno_Disciplina.Id }, aluno_Disciplina);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Aluno_Disciplina>> DeleteAluno_Disciplina(int id)
        {
            var aluno_Disciplina = await _context.Aluno_Disciplina
                        .Include(a => a.Aluno)
                        .Include(d => d.Disciplina)
                        .Include(p => p.Disciplina.Professor)
                        .Where(cod => cod.Id == id).FirstAsync();
            if (aluno_Disciplina == null)
            {
                return NotFound();
            }

            _context.Aluno_Disciplina.Remove(aluno_Disciplina);
            await _context.SaveChangesAsync();

            return aluno_Disciplina;
        }

        private bool Aluno_DisciplinaExists(int id)
        {
            return _context.Aluno_Disciplina.Any(e => e.Id == id);
        }
    }
}
