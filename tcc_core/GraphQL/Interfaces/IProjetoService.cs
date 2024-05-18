using tcc_core.Models;

namespace tcc_core.GraphQL.Interfaces
{
    public interface IProjetoService
    {
        IEnumerable<Projeto> GetAllProjeto();
        Projeto GetProjetoById(int id);
        Projeto CreateProjeto(Projeto projeto);
        Projeto UpdateProjeto(int id, Projeto projeto);
        void DeleteProjeto(int id);
    }
}
