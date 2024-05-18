using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class UsuarioQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<UsuarioModel> GetUsuario([Service] AppDbContext context)
        {
            return context.Usuario;
        }

      
    }
}
