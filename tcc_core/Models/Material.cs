namespace tcc_core.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Classificacao { get; set; }
        public decimal QuantidadeAtual { get; set; }

        public ICollection<MovimentacaoMaterial> MovimentacoesMateriais { get; set; }
    }
}
