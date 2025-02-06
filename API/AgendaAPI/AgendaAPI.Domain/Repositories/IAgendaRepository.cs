using AgendaAPI.Domain.Entities.Agenda;

namespace AgendaAPI.Domain.Repositories
{
    /// <summary>
    /// Interface da agenda
    /// </summary>
    public interface IAgendaRepository
    {
        Task<Horario> CadastraHorarioAsync(Horario horario);


    }
}
