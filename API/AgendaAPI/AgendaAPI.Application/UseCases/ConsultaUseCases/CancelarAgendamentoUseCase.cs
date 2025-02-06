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

        public async Task<bool> ExecuteAsync(int idAgendamento, string justificativa)
        {
            return await _consultaService.CancelarAgendamentoAsync(idAgendamento, justificativa);
        }
    }
}
