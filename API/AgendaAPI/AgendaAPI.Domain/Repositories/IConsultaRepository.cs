using AgendaAPI.Domain.Entities.Agenda;

namespace AgendaAPI.Domain.Repositories
{
    public interface IConsultaRepository
    {
        Task<List<Horario>> ObterHorariosDisponiveisAsync(int idMedico);

        Task<Agendamento> CadastrarAgendamentoAsync(Agendamento agendamento);

        Task<bool> CancelarAgendamentoAsync(int idAgendamento, string justificativa);
    }
}
