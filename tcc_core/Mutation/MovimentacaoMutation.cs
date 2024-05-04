using tcc_core.Data;
using Microsoft.EntityFrameworkCore;
using tcc_core.Models;

namespace tcc_core.Mutation
{
    public class MovimentacaoMutation
    {
        public async Task<Movimentacao> AddMovimentacao([Service] AppDbContext context, Movimentacao movimentacao)
        {
            context.Movimentacoes.Add(movimentacao);
            await context.SaveChangesAsync();
            return movimentacao;
        }

        public async Task<Movimentacao> UpdateMovimentacao([Service] AppDbContext context, int id, Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                throw new GraphQLException("IDs não coincidem.");
            }

            context.Entry(movimentacao).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Movimentacoes.Any(e => e.Id == id))
                {
                    throw new GraphQLException("Movimentação não encontrada.");
                }
                else
                {
                    throw;
                }
            }

            return movimentacao;
        }

        public async Task<Movimentacao> DeleteMovimentacao([Service] AppDbContext context, int id)
        {
            var movimentacao = await context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
            {
                throw new GraphQLException("Movimentação não encontrada.");
            }

            context.Movimentacoes.Remove(movimentacao);
            await context.SaveChangesAsync();

            return movimentacao;
        }
    }
}