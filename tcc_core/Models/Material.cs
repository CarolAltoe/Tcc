using System.ComponentModel.DataAnnotations;
using tcc_core.Models.Enuns;

namespace tcc_core.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A classificação é obrigatória.")]
        public ClassificacaoMaterial Classificacao { get; set; }
        public decimal QuantidadeAtual { get; set; }
        public bool IsAtivo { get; set; } = true;
        public ICollection<MovimentacaoMaterial> MovimentacaoMaterial { get; set; } = new List<MovimentacaoMaterial>();
    }
}
