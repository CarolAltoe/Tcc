using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        public string ResponsavelInterno { get; set; } = string.Empty;
        public string ResponsavelExterno { get; set; } = string.Empty;
        public string Turma { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Feedback { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public int QtdPessoas { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto Projeto { get; set; } = new Projeto();

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
    }
}
