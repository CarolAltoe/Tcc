using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IUsuarioRepository 
    {
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int id);
        Usuario CreateUsuario(Usuario usuario);
        Usuario UpdateUsuario(int id, Usuario usuario);
        void DeleteUsuario(int id);


    }
}
