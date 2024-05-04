using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Agendamento> GetAllAgendamentos()
        {
            return _context.Agendamentos.ToList();
        }

        public Agendamento GetAgendamentoById(int id)
        {
            return _context.Agendamentos.Find(id);
        }

        public Agendamento CreateAgendamento(Agendamento agendamento)
        {
            if (agendamento == null)
            {
                throw new ArgumentNullException(nameof(agendamento));
            }

            _context.Agendamentos.Add(agendamento);
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
            var agendamento = _context.Agendamentos.Find(id);
            if (agendamento == null)
            {
                throw new ArgumentException("Agendamento não encontrado.");
            }

            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();
        }
    }
}
