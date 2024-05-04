using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly AppDbContext _context;

        public ProjetoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Projeto> GetAllProjetos()
        {
            return _context.Projetos.ToList();
        }

        public Projeto GetProjetoById(int id)
        {
            return _context.Projetos.Find(id);
        }

        public Projeto CreateProjeto(Projeto projeto)
        {
            if (projeto == null)
            {
                throw new ArgumentNullException(nameof(projeto));
            }

            _context.Projetos.Add(projeto);
            _context.SaveChanges();
            return projeto;
        }

        public Projeto UpdateProjeto(int id, Projeto projeto)
        {
            if (id != projeto.Id)
            {
                throw new ArgumentException("ID do projeto não corresponde.");
            }

            _context.Entry(projeto).State = EntityState.Modified;
            _context.SaveChanges();
            return projeto;
        }

        public void DeleteProjeto(int id)
        {
            var projeto = _context.Projetos.Find(id);
            if (projeto == null)
            {
                throw new ArgumentException("Projeto não encontrado.");
            }

            _context.Projetos.Remove(projeto);
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