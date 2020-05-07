using ExemploEntityFramework.Models;

namespace ExemploEF.DTO
{
    public class AlunoDTO : Pessoa
    {
        public string Curso { get; set; }
    }
}