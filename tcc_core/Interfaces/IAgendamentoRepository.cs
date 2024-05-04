using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IAgendamentoRepository
    {
        IEnumerable<Agendamento> GetAllAgendamentos();
        Agendamento GetAgendamentoById(int id);
        Agendamento CreateAgendamento(Agendamento agendamento);
        Agendamento UpdateAgendamento(int id, Agendamento agendamento);
        void DeleteAgendamento(int id);
    }
}
