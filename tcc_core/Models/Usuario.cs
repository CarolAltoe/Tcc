namespace tcc_core.Models
{
     public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; }
        public ICollection<Movimentacao> Movimentacoes { get; set; }
    }
}
