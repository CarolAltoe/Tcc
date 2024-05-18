using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class MovimentacaoQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Movimentacao> GetMovimentacao([Service] AppDbContext context)
        {
            return context.Movimentacao;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Movimentacao GetMovimentacaoById([Service] AppDbContext context, int id)
        {
            return context.Movimentacao.FirstOrDefault(m => m.Id == id);
        }
    }
}
