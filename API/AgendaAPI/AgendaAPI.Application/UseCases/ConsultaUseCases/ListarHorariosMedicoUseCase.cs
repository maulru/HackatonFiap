using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;

namespace AgendaAPI.Application.UseCases.HorarioUseCases
{
    public class ListarHorariosMedicoUseCase
    {
        private readonly ConsultaServices _consultaService;

        public ListarHorariosMedicoUseCase(ConsultaServices consultaService)
        {
            _consultaService = consultaService;
        }

        public async Task<List<RetornoHorarioCadastrado>> ExecuteAsync(int idMedico)
        {
            return await _consultaService.ObterHorariosDisponiveisAsync(idMedico);
        }
    }
}
