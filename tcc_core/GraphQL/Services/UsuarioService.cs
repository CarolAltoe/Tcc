using Microsoft.EntityFrameworkCore;
using tcc_core.Models;
using tcc_core.Data;
using tcc_core.GraphQL.Interfaces;

namespace tcc_core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllUsuariosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<UsuarioModel> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<UsuarioModel> AddUsuarioAsync(UsuarioModel usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioModel> UpdateUsuarioAsync(int id, UsuarioModel usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }
            else if (id != usuario.Id)
            {
                throw new ArgumentException("ID do usuário não corresponde.");
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        internal string? Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}

