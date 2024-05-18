
using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;
using tcc_core.Services;

namespace tcc_core.Mutation
{
    public class UsuarioMutation
    {
        public async Task<UsuarioModel> AddUsuario([Service] UsuarioService usuarioService, UsuarioModel usuario)
        {
            return await usuarioService.AddUsuarioAsync(usuario);
        }

        public async Task<UsuarioModel> UpdateUsuario([Service] UsuarioService usuarioService, int id, UsuarioModel usuario)
        {
            return await usuarioService.UpdateUsuarioAsync(id, usuario);
        }

        public async Task DeleteUsuario([Service] UsuarioService usuarioService, int id)
        {
            await usuarioService.DeleteUsuarioAsync(id);
        }
    }
}