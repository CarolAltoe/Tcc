using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class ProjetoQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Projeto> GetProjeto([Service] AppDbContext context)
        {
            return context.Projeto;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Projeto GetProjetoById([Service] AppDbContext context, int id)
        {
            return context.Projeto.FirstOrDefault(p => p.Id == id);
        }
    }
}