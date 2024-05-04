namespace tcc_core.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string RespInterno { get; set; }
        public string RespExterno { get; set; }
        public string Turma { get; set; }
        public string Descricao { get; set; }
        public string Feedback { get; set; }
        public string Observacoes { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public int QtdPessoas { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
