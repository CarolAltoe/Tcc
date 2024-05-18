using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Services
{
    public class UserTesteService
    {
        private readonly AppDbContext _contexto;

        public UserTesteService(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<UserTeste> ObterTodosUserTestes()
        {
            return _contexto.UserTestes.AsQueryable();
        }

        public UserTeste ObterUserTestePorId(int id)
        {
            return _contexto.UserTestes.FirstOrDefault(u => u.Id == id);
        }

        public void AdicionarUserTeste(UserTeste usuario)
        {
            _contexto.UserTestes.Add(usuario);
            _contexto.SaveChanges();
        }
    }
}
