using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IAgendamentoService
    {
        IEnumerable<Agendamento> GetAllAgendamento();
        Agendamento GetAgendamentoById(int id);
        Agendamento CreateAgendamento(Agendamento agendamento);
        Agendamento UpdateAgendamento(int id, Agendamento agendamento);
        void DeleteAgendamento(int id);
    }
}
