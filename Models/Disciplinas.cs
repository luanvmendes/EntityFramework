using ExemploEntityFramework.Models;

namespace ExemploEF.Models
{
    public class Disciplinas
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Professor Professor { get; set; }
    }
}