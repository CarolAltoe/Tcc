using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
     public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public ICollection<Agendamento>? Agendamento { get; set; } = new List<Agendamento>();
        public ICollection<Movimentacao>? Movimentacao { get; set; } = new List<Movimentacao>();
    }
}
