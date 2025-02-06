using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class ObterHorariosPendentesOuAgendadosUseCase
    {
        private readonly AgendaServices _agendaService;

        public ObterHorariosPendentesOuAgendadosUseCase(AgendaServices agendaService)
        {
            _agendaService = agendaService;
        }

        public async Task<List<RetornoHorarioCadastrado>> ExecuteAsync(int idMedico)
        {
            return await _agendaService.ObterHorariosPendentesOuAgendadosAsync(idMedico);
        }
    }
}
