using ExemploEntityFramework.Models;

namespace ExemploEF.DTO
{
    public class ProfessorDTO : Pessoa
    {
        public string Especialidade { get; set; }
    }
}