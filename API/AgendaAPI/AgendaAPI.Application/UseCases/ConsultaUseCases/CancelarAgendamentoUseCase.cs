using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.Services;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class CancelarAgendamentoUseCase
    {
        private readonly ConsultaServices _consultaService;

        public CancelarAgendamentoUseCase(ConsultaServices consultaService)
        {
            _consultaService = consultaService;
        }

        public async Task<bool> ExecuteAsync(CancelarAgendamentoDTO cancelarAgendamentoDTO)
        {
            return await _consultaService.CancelarAgendamentoAsync(cancelarAgendamentoDTO);
        }
    }
}
