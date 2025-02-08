using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Application.UseCases.HorarioUseCases;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ConsultaController : ControllerBase
    {
        #region Propriedades
        private readonly ListarMedicoUseCase _listarMedicoUseCase;
        private readonly ListarHorariosMedicoUseCase _listarHorariosMedicoUseCase;
        private readonly CadastrarAgendamentoUseCase _cadastrarAgendamentoUseCase;
        private readonly CancelarAgendamentoUseCase _cancelarAgendamentoUseCase;
        #endregion

        #region Construtores
        public ConsultaController(
            ListarMedicoUseCase listarMedicoUseCase,
            ListarHorariosMedicoUseCase listarHorariosMedicoUseCase,
            CadastrarAgendamentoUseCase cadastrarAgendamentoUseCase,
            CancelarAgendamentoUseCase cancelarAgendamentoUseCase)
        {
            _listarMedicoUseCase = listarMedicoUseCase;
            _listarHorariosMedicoUseCase = listarHorariosMedicoUseCase;
            _cadastrarAgendamentoUseCase = cadastrarAgendamentoUseCase;
            _cancelarAgendamentoUseCase = cancelarAgendamentoUseCase;
        }
        #endregion

        #region Actions

        /// <summary>
        /// Endpoint responsável por listar os horários disponíveis para o médico informado.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     GET /Consulta/HorariosDisponiveis?idMedico=1
        /// </remarks>
        /// <param name="idMedico">Identificador do médico.</param>
        /// <returns>Lista de horários disponíveis para o médico.</returns>
        [HttpGet("HorariosDisponiveis")]
        [AuthorizePaciente]
        public async Task<IActionResult> ObterHorariosDisponiveis([FromQuery] int idMedico)
        {
            var horarios = await _listarHorariosMedicoUseCase.ExecuteAsync(idMedico);
            if (horarios == null || horarios.Count == 0)
                return NotFound("Nenhum horário disponível encontrado para este médico.");

            return Ok(horarios);
        }

        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um agendamento.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Consulta/CadastrarAgendamento
        ///     {
        ///         "idHorario": 5
        ///     }
        /// </remarks>
        /// <param name="agendamentoDTO">Objeto com as informações para o cadastro do agendamento.</param>
        /// <returns>Dados do agendamento cadastrado.</returns>
        [HttpPost("CadastrarAgendamento")]
        [AuthorizePaciente]
        [Consumes("application/json")]
        public async Task<IActionResult> CadastrarAgendamento([FromBody] CadAgendamentoDTO agendamentoDTO)
        {
            if (HttpContext.Items["IdPaciente"] is string idPacienteString &&
                int.TryParse(idPacienteString, out int idPaciente))
            {
                agendamentoDTO.IdPaciente = idPaciente;
            }
            else
            {
                return Forbid("IdPaciente não encontrado.");
            }

            var agendamento = await _cadastrarAgendamentoUseCase.ExecuteAsync(agendamentoDTO);
            if (agendamento == null)
                return BadRequest("O horário selecionado não está disponível.");

            return CreatedAtAction(nameof(CadastrarAgendamento), new { id = agendamento.Id }, agendamento);
        }

        /// <summary>
        /// Endpoint responsável por realizar o cancelamento de um agendamento.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     PUT /Consulta/CancelarAgendamento
        ///     {
        ///         "idAgendamento": 123,
        ///         "justificativa": "Não poderei comparecer ao compromisso."
        ///     }
        /// </remarks>
        /// <param name="cancelarAgendamentoDTO">
        /// Objeto contendo o identificador do agendamento e a justificativa para o cancelamento.
        /// </param>
        /// <returns>Mensagem indicando o sucesso ou erro do cancelamento.</returns>
        [HttpPut("CancelarAgendamento")]
        [AuthorizePaciente]
        [Consumes("application/json")]
        public async Task<IActionResult> CancelarAgendamento([FromBody] CancelarAgendamentoDTO cancelarAgendamentoDTO)
        {
            if (HttpContext.Items["IdPaciente"] is string idPacienteString &&
                int.TryParse(idPacienteString, out int idPaciente))
            {
                cancelarAgendamentoDTO.IdPaciente = idPaciente;
            }
            else
            {
                return Forbid("IdPaciente não encontrado.");
            }


            if (string.IsNullOrWhiteSpace(cancelarAgendamentoDTO.Justificativa))
                return BadRequest("A justificativa para o cancelamento é obrigatória.");

            bool sucesso = await _cancelarAgendamentoUseCase.ExecuteAsync(cancelarAgendamentoDTO);

            if (!sucesso)
                return NotFound("Agendamento não encontrado ou já cancelado.");

            return Ok("Agendamento cancelado com sucesso.");
        }

        #endregion
    }
}
