using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
    public class Movimentacao
    {
        [Key] 
        public int Id { get; set; }
        public string Responsavelonsavel { get; set; } = string.Empty;
        public string TipoMovimentacao { get; set; } = string.Empty;
        public DateTime DtMovimentacao { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; } = new UsuarioModel();

        public int? ProjetoId { get; set; }
        public Projeto Projeto { get; set; } = new Projeto();

        public ICollection<MovimentacaoMaterial> MovimentacaoMaterial { get; set; } = new List<MovimentacaoMaterial>();
    }
}
