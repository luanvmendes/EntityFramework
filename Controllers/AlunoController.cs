using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploEntityFramework.Models;

namespace ExemploEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlunoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno()
        {
            return Ok(await _context.Aluno.Select(dados => new {
                dados.RA,
                dados.Nome,
                Endereco = dados.Endereco.Rua + ", " + dados.Endereco.Numero,
                Bairro = dados.Endereco.Bairro,
                Cidade = dados.Endereco.Cidade,
                Estado = dados.Endereco.Estado
            }).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            if (_context.Aluno.Where(matricula => matricula.RA == id).Count() != 0)
            {
                return Ok(await _context.Aluno.Where(matricula => matricula.RA == id).Select(dados => new {
                    dados.RA,
                    dados.Nome,
                    Endereco = dados.Endereco.Rua + ", " + dados.Endereco.Numero,
                    Bairro = dados.Endereco.Bairro,
                    Cidade = dados.Endereco.Cidade,
                    Estado = dados.Endereco.Estado
                }).ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            aluno.RA = id;

            if (AlunoExists(id))
            {
                var student = _context.Aluno.Include(e => e.Endereco).First(id => id.RA == aluno.RA);
                student.Nome = aluno.Nome;
                student.Endereco.Rua = aluno.Endereco.Rua;
                student.Endereco.Numero = aluno.Endereco.Numero;
                student.Endereco.Bairro = aluno.Endereco.Bairro;
                student.Endereco.Cidade = aluno.Endereco.Cidade;
                student.Endereco.Estado = aluno.Endereco.Estado;

                _context.Entry(student).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlunoExists(aluno.RA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAluno", new { id = aluno.RA }, aluno);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Aluno>> DeleteAluno(int id)
        {
            var aluno = await _context.Aluno.Include(e => e.Endereco)
                        .Where(matricula => matricula.RA == id).SingleOrDefaultAsync();
            if (aluno == null)
            {
                return NotFound();
            }

            var endereco = await _context.Endereco.FindAsync(aluno.Endereco.Id);

            _context.Aluno.Remove(aluno);
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();

            return aluno;
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.RA == id);
        }
    }
}
