using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc_core.Models
{
    public class MovimentacaoMaterial
    {
        [Key, Column(Order = 0)]
        public int MovimentacaoId { get; set; }
        public Movimentacao? Movimentacao { get; set; }

        [Key, Column(Order = 1)]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]

        public decimal Quantidade { get; set; }
    }
}
