using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExemploEntityFramework.Models
{
    public class Aluno
    {
        [Key]
        public int RA { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }

    }
}