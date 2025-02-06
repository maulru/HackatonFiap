using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Application.UseCases.HorarioUseCases;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConsultaController : ControllerBase
    {

        private readonly ListarMedicoUseCase _listarMedicoUseCase;
        private readonly ListarHorariosMedicoUseCase _listarHorariosMedicoUseCase;
        private readonly CadastrarAgendamentoUseCase _cadastrarAgendamentoUseCase;
        private readonly CancelarAgendamentoUseCase _cancelarAgendamentoUseCase;
      

        public ConsultaController(ListarMedicoUseCase listarMedicoUseCase,
            ListarHorariosMedicoUseCase listarHorariosMedicoUseCase,
            CadastrarAgendamentoUseCase cadastrarAgendamentoUseCase,
            CancelarAgendamentoUseCase cancelarAgendamentoUseCase) 
        {
            _listarMedicoUseCase = listarMedicoUseCase;
            _listarHorariosMedicoUseCase = listarHorariosMedicoUseCase;
            _cadastrarAgendamentoUseCase = cadastrarAgendamentoUseCase;
            _cancelarAgendamentoUseCase = cancelarAgendamentoUseCase;
        }


        [HttpGet("HorariosDisponiveis")]
        public async Task<IActionResult> ObterHorariosDisponiveis([FromQuery] int idMedico)
        {
            var horarios = await _listarHorariosMedicoUseCase.ExecuteAsync(idMedico);
            if (horarios == null || horarios.Count == 0)
                return NotFound("Nenhum horário disponível encontrado para este médico.");

            return Ok(horarios);
        }

        [HttpPost("CadastrarAgendamento")]
        public async Task<IActionResult> CadastrarAgendamento([FromBody] CadAgendamentoDTO agendamentoDTO)
        {
            var agendamento = await _cadastrarAgendamentoUseCase.ExecuteAsync(agendamentoDTO);
            if (agendamento == null)
                return BadRequest("O horário selecionado não está disponível.");

            return CreatedAtAction(nameof(CadastrarAgendamento), new { id = agendamento.Id }, agendamento);
        }

        [HttpPut("CancelarAgendamento")]
        public async Task<IActionResult> CancelarAgendamento([FromQuery] int idAgendamento, [FromQuery] string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
                return BadRequest("A justificativa para o cancelamento é obrigatória.");

            bool sucesso = await _cancelarAgendamentoUseCase.ExecuteAsync(idAgendamento, justificativa);

            if (!sucesso)
                return NotFound("Agendamento não encontrado ou já cancelado.");

            return Ok("Agendamento cancelado com sucesso.");
        }

        /*
         * ListarMedicos (por especialidade) (acionar endpoint da outra API)
         * ListaHorariosMedico
         * Cadastrar Agendamento
         * Cancelar Agendamento
         * 
         */


    }
}
