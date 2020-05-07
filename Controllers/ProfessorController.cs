using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploEntityFramework.Models;
using ExemploEF.DTO;

namespace ExemploEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessor()
        {
            return await _context.Professor.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessor(int id)
        {
            var professor = await _context.Professor.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            return professor;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, ProfessorDTO professorDTO)
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            professor.Nome = professorDTO.Nome;
            professor.Rua = professorDTO.Rua;
            professor.Numero = professorDTO.Numero;
            professor.Bairro = professorDTO.Bairro;
            professor.Cidade = professorDTO.Cidade;
            professor.Estado = professorDTO.Estado;
            professor.Especialidade = professorDTO.Especialidade;

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProfessorExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProfessorDTO>> PostProfessor(ProfessorDTO professorDTO)
        {
            var professor = new Professor
            {
                Nome = professorDTO.Nome,
                Rua = professorDTO.Rua,
                Numero = professorDTO.Numero,
                Bairro = professorDTO.Bairro,
                Cidade = professorDTO.Cidade,
                Estado = professorDTO.Estado,
                Especialidade = professorDTO.Especialidade
            };

            _context.Professor.Add(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessor", new { id = professor.Id }, professor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Professor>> DeleteProfessor(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();

            return professor;
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.Id == id);
        }
    }
}
