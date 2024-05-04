
namespace tcc_core.Mutation
{
    public class RootMutation
    {
        public UsuarioMutation UsuarioMutation { get; }
        public ProjetoMutation ProjetoMutation { get; }
        public AgendamentoMutation AgendamentoMutation { get; }
        public MaterialMutation MaterialMutation { get; }
        public MovimentacaoMutation MovimentacaoMutation { get; }

        public RootMutation(UsuarioMutation usuarioMutation, ProjetoMutation projetoMutation,
               AgendamentoMutation agendamentoMutation, MaterialMutation materialMutation,
               MovimentacaoMutation movimentacaoMutation)
        {
            UsuarioMutation = usuarioMutation;
            ProjetoMutation = projetoMutation;
            AgendamentoMutation = agendamentoMutation;
            MaterialMutation = materialMutation;
            MovimentacaoMutation = movimentacaoMutation;
        }
    }
}
