using System.ComponentModel.DataAnnotations;

namespace ExemploEntityFramework.Models
{
    public class Aluno : Pessoa
    {
        [Key]
        public int RA { get; set; }
        public string Curso { get; set; }
    }
}