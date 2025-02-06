using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Domain.Repositories
{
    /// <summary>
    /// Interface da agenda
    /// </summary>
    public interface IAgendaRepository
    {
        Task<Horario> CadastraHorarioAsync(Horario horario);

        Task<List<Horario>> ObterHorariosAsync(int IdMedico);

        Task<Horario> AlterarHorarioAsync(Horario horario);

        Task<List<Horario>> ObterHorariosPendentesOuAgendadosAsync(int idMedico);
        Task<bool> AlterarStatusAgendamentoAsync(int idAgendamento, Disponibilidade novoStatus, string observacoes = "");


    }
}
