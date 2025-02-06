using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class ObterHorariosDisponiveisUseCase
    {
        private readonly AgendaServices _agendaService;

        public ObterHorariosDisponiveisUseCase(AgendaServices agendaService)
        {
            _agendaService = agendaService;
        }

        public async Task<List<RetornoHorarioCadastrado>> ExecuteAsync(int idMedico)
        {
            return await _agendaService.ObterHorariosDisponiveisAsync(idMedico);
        }
    }
}
