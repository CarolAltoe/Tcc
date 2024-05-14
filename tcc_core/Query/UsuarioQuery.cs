using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class UsuarioQuery
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<Usuario> GetUsuario([Service] AppDbContext context)
        {
            return context.Usuario;
        }

        public async Task<Usuario> GetUsuarioById([Service] AppDbContext context, int id)
        {
            return await context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
