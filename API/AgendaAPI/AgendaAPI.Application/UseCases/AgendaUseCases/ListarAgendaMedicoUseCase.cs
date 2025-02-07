using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;
using AgendaAPI.Domain.Entities.Agenda;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class ListarAgendaMedicoUseCase
    {
        private readonly AgendaServices _agendaServices;

        public ListarAgendaMedicoUseCase(AgendaServices agendaServices)
        {
            _agendaServices = agendaServices;
        }

        public async Task<List<RetornoHorarioCadastrado>> ObterHorariosAsync(int idMedico)
        {
            return await _agendaServices.ObterHorariosAsync(idMedico);
        }

    }
}
