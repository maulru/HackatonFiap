using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Base;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.DTOs.Medico;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Domain.Enums.Agenda;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AgendaController : ControllerBase
    {

        private readonly CadastrarHorarioUseCase _cadastrarHorarioUseCase;
        private readonly ListarAgendaMedicoUseCase _listarAgendaMedicoUseCase;
        private readonly AlterarHorarioUseCase _alterarHorarioUseCase;
        private readonly AlterarStatusAgendamentoUseCase _alterarStatusAgendamentoUseCase;
        private readonly ObterHorariosPendentesOuAgendadosUseCase _obterHorariosPendentesOuAgendadosUseCase;

        public AgendaController(CadastrarHorarioUseCase cadastrarHorarioUseCase,
            ListarAgendaMedicoUseCase listarAgendaMedicoUseCase,
            AlterarHorarioUseCase alterarHorarioUseCase,
            AlterarStatusAgendamentoUseCase alterarStatusAgendamentoUseCase,
            ObterHorariosPendentesOuAgendadosUseCase obterHorariosPendentesOuAgendadosUseCase)
        {
            _cadastrarHorarioUseCase = cadastrarHorarioUseCase;
            _listarAgendaMedicoUseCase = listarAgendaMedicoUseCase;
            _alterarHorarioUseCase = alterarHorarioUseCase;
            _alterarStatusAgendamentoUseCase = alterarStatusAgendamentoUseCase;
            _obterHorariosPendentesOuAgendadosUseCase = obterHorariosPendentesOuAgendadosUseCase;

        }

        [HttpPost("CadastrarHorario/")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoHorarioCadastrado), 201)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        public async Task<IActionResult> CadastrarHorario([FromBody] CadAgendaDTO cadAgendaDTO)
        {
            RetornoHorarioCadastrado horarioCadastrado = await _cadastrarHorarioUseCase.CadastrarHorario(cadAgendaDTO);
            return CreatedAtAction(nameof(CadastrarHorario), new { id = horarioCadastrado });
        }

        /// <summary>
        /// Action responsável por listar os agendamentos do médico, confirmados e disponíveis
        /// </summary>
        /// <param name="medicoDTO"></param>
        /// <returns></returns>
        [HttpGet("Horarios/")]
        public async Task<IActionResult> ListarAgendaMedico(int idMedico)
        {
            List<RetornoHorarioCadastrado> agendaMedico = await _listarAgendaMedicoUseCase.ObterHorariosAsync(idMedico);
            return Ok(agendaMedico);
        }

        [HttpPut("AlterarHorario/")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoHorarioCadastrado), 200)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        [ProducesResponseType(typeof(RetornoErroDTO), 404)]
        public async Task<IActionResult> AlterarHorario([FromBody] CadAgendaDTO agendaDTO)
        {
            var horarioAlterado = await _alterarHorarioUseCase.AlterarHorario(agendaDTO);

            if (horarioAlterado == null)
                return NotFound(new { mensagem = "Horário não encontrado." });

            return Ok(horarioAlterado);
        }

        [HttpGet("HorariosPendentesOuAgendados")]
        public async Task<IActionResult> ObterHorariosPendentesOuAgendados([FromQuery] int idMedico)
        {
            var horarios = await _obterHorariosPendentesOuAgendadosUseCase.ExecuteAsync(idMedico);
            return Ok(horarios);
        }

        [HttpPut("AlterarStatusAgendamento")]
        public async Task<IActionResult> AlterarStatusAgendamento([FromQuery] int idAgendamento, [FromQuery] Disponibilidade novoStatus, [FromQuery] string observacoes = "")
        {
            bool sucesso = await _alterarStatusAgendamentoUseCase.ExecuteAsync(idAgendamento, novoStatus, observacoes);
            if (!sucesso)
                return NotFound("Agendamento ou horário não encontrado.");

            return Ok("Status atualizado com sucesso.");
        }



    }
}
