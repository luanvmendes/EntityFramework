using ExemploEntityFramework.Models;

namespace ExemploEF.Models
{
    public class Aluno_Disciplina
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplinas Disciplina { get; set; }
    }
}