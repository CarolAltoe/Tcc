
using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Mutation
{
    public class UsuarioMutation
    {
        public async Task<Usuario> AddUsuario([Service] AppDbContext context, Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateUsuario([Service] AppDbContext context, int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                throw new GraphQLException("IDs não coincidem.");
            }

            context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Usuarios.Any(e => e.Id == id))
                {
                    throw new GraphQLException("Usuário não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return usuario;
        }

        public async Task<Usuario> DeleteUsuario([Service] AppDbContext context, int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                throw new GraphQLException("Usuário não encontrado.");
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return usuario;
        }
    }
}