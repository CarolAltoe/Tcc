using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IMovimentacaoRepository
    {
        IEnumerable<Movimentacao> GetAllMovimentacoes();
        Movimentacao GetMovimentacaoById(int id);
        Movimentacao CreateMovimentacao(Movimentacao movimentacao);
        Movimentacao UpdateMovimentacao(int id, Movimentacao movimentacao);
        void DeleteMovimentacao(int id);
    }
}
