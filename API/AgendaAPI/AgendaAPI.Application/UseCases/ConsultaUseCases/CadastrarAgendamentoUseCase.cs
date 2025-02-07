using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.Services;
using System.Threading.Tasks;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class CadastrarAgendamentoUseCase
    {
        private readonly ConsultaServices _consultaService;

        public CadastrarAgendamentoUseCase(ConsultaServices consultaService)
        {
            _consultaService = consultaService;
        }

        public async Task<RetornoAgendamentoDTO> ExecuteAsync(CadAgendamentoDTO agendamentoDTO)
        {
            return await _consultaService.CadastrarAgendamentoAsync(agendamentoDTO);
        }
    }
}
