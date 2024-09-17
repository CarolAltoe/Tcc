using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc_core.Models
{
     public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo E-mail deve ter um endereço de e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        //[DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        public ICollection<Agendamento>? Agendamento { get; set; } = new List<Agendamento>();
        public ICollection<Movimentacao>? Movimentacao { get; set; } = new List<Movimentacao>();

        public bool IsActive { get; set; } = false;
    }
}
