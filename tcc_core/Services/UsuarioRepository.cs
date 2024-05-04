using Microsoft.EntityFrameworkCore;
using tcc_core.Models;
using tcc_core.Interfaces;
using tcc_core.Data;

namespace tcc_core.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario CreateUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                throw new ArgumentException("ID do usuário não corresponde.");
            }

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
            return usuario;
        }

        public void DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}

/*
 
 
    private static List<User> UserList = new List<User>()
        {
            new User() { Id = 1, Name = "Classic Burger", Description="A juicy chicken burger with lettuce and cheese" , Price = 8.99},
            new User() { Id = 2, Name = "Margherita Pizza", Description = "Tomato, mozzarella, and basil pizza", Price = 10.50 },
            new User() { Id = 3, Name = "Grilled Chicken Salad", Description = "Fresh garden salad with grilled chicken", Price = 7.95 },
            new User() { Id = 4, Name = "Pasta Alfredo", Description = "Creamy Alfredo sauce with fettuccine pasta", Price = 12.75 },
            new User() { Id = 5, Name = "Chocolate Brownie Sundae", Description = "Warm chocolate brownie with ice cream and fudge", Price = 6.99 },
        };*/