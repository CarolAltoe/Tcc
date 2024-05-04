using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class ProjetoQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Projeto> GetProjetos([Service] AppDbContext context)
        {
            return context.Projetos;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Projeto GetProjetoById([Service] AppDbContext context, int id)
        {
            return context.Projetos.FirstOrDefault(p => p.Id == id);
        }
    }
}