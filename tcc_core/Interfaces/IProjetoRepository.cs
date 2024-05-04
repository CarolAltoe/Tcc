using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IProjetoRepository
    {
        IEnumerable<Projeto> GetAllProjetos();
        Projeto GetProjetoById(int id);
        Projeto CreateProjeto(Projeto projeto);
        Projeto UpdateProjeto(int id, Projeto projeto);
        void DeleteProjeto(int id);
    }
}
