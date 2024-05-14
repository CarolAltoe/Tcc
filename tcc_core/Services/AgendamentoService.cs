using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly AppDbContext _context;

        public AgendamentoService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Agendamento> GetAllAgendamento()
        {
            return _context.Agendamento.ToList();
        }

        public Agendamento GetAgendamentoById(int id)
        {
            return _context.Agendamento.Find(id);
        }

        public Agendamento CreateAgendamento(Agendamento agendamento)
        {
            if (agendamento == null)
            {
                throw new ArgumentNullException(nameof(agendamento));
            }

            _context.Agendamento.Add(agendamento);
            _context.SaveChanges();
            return agendamento;
        }

        public Agendamento UpdateAgendamento(int id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                throw new ArgumentException("ID do agendamento não corresponde.");
            }

            _context.Entry(agendamento).State = EntityState.Modified;
            _context.SaveChanges();
            return agendamento;
        }

        public void DeleteAgendamento(int id)
        {
            var agendamento = _context.Agendamento.Find(id);
            if (agendamento == null)
            {
                throw new ArgumentException("Agendamento não encontrado.");
            }

            _context.Agendamento.Remove(agendamento);
            _context.SaveChanges();
        }
    }
}
