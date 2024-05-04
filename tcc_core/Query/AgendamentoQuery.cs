using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class AgendamentoQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Agendamento> GetAgendamentos([Service] AppDbContext context)
        {
            return context.Agendamentos;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Agendamento GetAgendamentoById([Service] AppDbContext context, int id)
        {
            return context.Agendamentos.FirstOrDefault(a => a.Id == id);
        }
    }
}
