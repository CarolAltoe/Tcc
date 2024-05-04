using System.ComponentModel.DataAnnotations.Schema;

namespace tcc_core.Models
{
    public class MovimentacaoMaterial
    {
        public int MovimentacaoId { get; set; }
        public Movimentacao Movimentacao { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public decimal Quantidade { get; set; }
    }
}
