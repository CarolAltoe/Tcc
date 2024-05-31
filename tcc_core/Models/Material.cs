using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "A classificação é obrigatória.")]
        public string Classificacao { get; set; } = string.Empty;
        public decimal QuantidadeAtual { get; set; }

        public ICollection<MovimentacaoMaterial> MovimentacaoMaterial { get; set; } = new List<MovimentacaoMaterial>();
    }
}
