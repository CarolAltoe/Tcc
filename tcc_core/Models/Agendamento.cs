using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc_core.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O responsável interno é obrigatório.")]
        public string ResponsavelInterno { get; set; } = string.Empty;
        [Required(ErrorMessage = "O responsável externo é obrigatório.")]
        public string ResponsavelExterno { get; set; } = string.Empty;
        [Required(ErrorMessage = "A turma é obrigatória.")]
        public string Turma { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? Feedback { get; set; } = string.Empty;
        public string? Observacoes { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data inicial é obrigatória.")]
        public DateTime DtInicial { get; set; }
        [Required(ErrorMessage = "A data de encerramento é obrigatória.")]
        public DateTime DtFinal { get; set; }
        [Required(ErrorMessage = "A quantidade de pessoas é obrigatória.")]
        public int QtdPessoas { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
    }
}
