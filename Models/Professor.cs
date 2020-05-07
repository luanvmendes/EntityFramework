namespace ExemploEntityFramework.Models
{
    public class Professor : Pessoa
    {
        public int Id { get; set; }
        public string Especialidade { get; set; }
    }
}