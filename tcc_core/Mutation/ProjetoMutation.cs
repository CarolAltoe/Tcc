using tcc_core.Data; 
using Microsoft.EntityFrameworkCore;
using tcc_core.Models;

namespace tcc_core.Mutation
{
    public class ProjetoMutation
    {
        public async Task<Projeto> AddProjeto([Service] AppDbContext context, Projeto projeto)
        {
            context.Projetos.Add(projeto);
            await context.SaveChangesAsync();
            return projeto;
        }

        public async Task<Projeto> UpdateProjeto([Service] AppDbContext context, int id, Projeto projeto)
        {
            if (id != projeto.Id)
            {
                throw new GraphQLException("IDs não coincidem.");
            }

            context.Entry(projeto).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Projetos.Any(e => e.Id == id))
                {
                    throw new GraphQLException("Projeto não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return projeto;
        }

        public async Task<Projeto> DeleteProjeto([Service] AppDbContext context, int id)
        {
            var projeto = await context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                throw new GraphQLException("Projeto não encontrado.");
            }

            context.Projetos.Remove(projeto);
            await context.SaveChangesAsync();

            return projeto;
        }
    }
}