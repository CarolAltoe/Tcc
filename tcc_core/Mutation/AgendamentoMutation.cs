using tcc_core.Data;
using Microsoft.EntityFrameworkCore;
using tcc_core.Models;

namespace tcc_core.Mutation
{
    public class AgendamentoMutation
    {
        public async Task<Agendamento> AddAgendamento([Service] AppDbContext context, Agendamento agendamento)
        {
            context.Agendamentos.Add(agendamento);
            await context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<Agendamento> UpdateAgendamento([Service] AppDbContext context, int id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                throw new GraphQLException("IDs não coincidem.");
            }

            context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Agendamentos.Any(e => e.Id == id))
                {
                    throw new GraphQLException("Agendamento não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return agendamento;
        }

        public async Task<Agendamento> DeleteAgendamento([Service] AppDbContext context, int id)
        {
            var agendamento = await context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                throw new GraphQLException("Agendamento não encontrado.");
            }

            context.Agendamentos.Remove(agendamento);
            await context.SaveChangesAsync();

            return agendamento;
        }
    }
}