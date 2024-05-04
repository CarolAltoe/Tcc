using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class UsuarioQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Usuario> GetUsuarios([Service] AppDbContext context)
        {
            return context.Usuarios;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Usuario GetUsuarioById([Service] AppDbContext context, int id)
        {
            return context.Usuarios.FirstOrDefault(u => u.Id == id);
        }
    }
}