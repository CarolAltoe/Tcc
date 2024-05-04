
namespace tcc_core.Query
{
    public class RootQuery
    {
        public UsuarioQuery UsuarioQuery { get; }
        public ProjetoQuery ProjetoQuery { get; }
        public AgendamentoQuery AgendamentoQuery { get; }
        public MaterialQuery MaterialQuery { get; }
        public MovimentacaoQuery MovimentacaoQuery { get; }

        public RootQuery(UsuarioQuery usuarioQuery, ProjetoQuery projetoQuery,
               AgendamentoQuery agendamentoQuery, MaterialQuery materialQuery,
               MovimentacaoQuery movimentacaoQuery)
        {
            UsuarioQuery = usuarioQuery;
            ProjetoQuery = projetoQuery;
            AgendamentoQuery = agendamentoQuery;
            MaterialQuery = materialQuery;
            MovimentacaoQuery = movimentacaoQuery;
        }
    }
}
