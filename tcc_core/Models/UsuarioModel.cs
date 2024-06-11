using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
     public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        public ICollection<Agendamento>? Agendamento { get; set; } = new List<Agendamento>();
        public ICollection<Movimentacao>? Movimentacao { get; set; } = new List<Movimentacao>();
    }
}
