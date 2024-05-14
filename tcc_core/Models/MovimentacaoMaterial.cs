using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc_core.Models
{
    public class MovimentacaoMaterial
    {
        [Key]
        public int MovimentacaoId { get; set; }
        public Movimentacao Movimentacao { get; set; }

        [Key]
        public int MaterialId { get; set; }
        public Material Material { get; set; } 

        public decimal Quantidade { get; set; }
    }
}
