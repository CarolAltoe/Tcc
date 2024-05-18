using tcc_core.Models;

namespace tcc_core.GraphQL.Interfaces
{
    public interface IUsuarioService
    {


        Task<IEnumerable<UsuarioModel>> GetAllUsuariosAsync();
        Task<UsuarioModel> GetUsuarioByIdAsync(int id);
        Task<UsuarioModel> AddUsuarioAsync(UsuarioModel usuario);
        Task<UsuarioModel> UpdateUsuarioAsync(int id, UsuarioModel usuario);
        Task DeleteUsuarioAsync(int id);


    }
}
/*
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int id);
        Usuario AddUsuario(Usuario usuario);
        Usuario UpdateUsuario(int id, Usuario usuario);
        void DeleteUsuario(int id);*/