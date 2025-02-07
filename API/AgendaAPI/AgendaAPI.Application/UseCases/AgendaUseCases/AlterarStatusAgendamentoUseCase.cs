using AgendaAPI.Application.Services;
using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class AlterarStatusAgendamentoUseCase
    {
        private readonly AgendaServices _agendaService;

        public AlterarStatusAgendamentoUseCase(AgendaServices agendaService)
        {
            _agendaService = agendaService;
        }

        public async Task<bool> ExecuteAsync(int idAgendamento, Disponibilidade novoStatus, string observacoes = "")
        {
            return await _agendaService.AlterarStatusAgendamentoAsync(idAgendamento, novoStatus, observacoes);
        }
    }
}
