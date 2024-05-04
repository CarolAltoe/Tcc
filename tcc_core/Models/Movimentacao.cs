namespace tcc_core.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string Responsavel { get; set; }
        public string TipoMovimentacao { get; set; }
        public DateTime DtMovimentacao { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public ICollection<MovimentacaoMaterial> MovimentacoesMateriais { get; set; }
    }
}
