
using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;
using tcc_core.Services;

namespace tcc_core.Mutation
{
    public class UsuarioMutation
    {
        public async Task<Usuario> AddUsuario([Service] UsuarioService usuarioService, Usuario usuario)
        {
            return await usuarioService.AddUsuarioAsync(usuario);
        }

        public async Task<Usuario> UpdateUsuario([Service] UsuarioService usuarioService, int id, Usuario usuario)
        {
            return await usuarioService.UpdateUsuarioAsync(id, usuario);
        }

        public async Task DeleteUsuario([Service] UsuarioService usuarioService, int id)
        {
            await usuarioService.DeleteUsuarioAsync(id);
        }
    }
}