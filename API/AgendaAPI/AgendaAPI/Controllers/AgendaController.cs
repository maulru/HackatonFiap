using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Base;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using AgendaAPI.Domain.Enums.Agenda;
using AgendaAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        #region Propriedades
        private readonly CadastrarHorarioUseCase _cadastrarHorarioUseCase;
        private readonly ListarAgendaMedicoUseCase _listarAgendaMedicoUseCase;
        private readonly AlterarHorarioUseCase _alterarHorarioUseCase;
        private readonly AlterarStatusAgendamentoUseCase _alterarStatusAgendamentoUseCase;
        private readonly ObterHorariosPendentesOuAgendadosUseCase _obterHorariosPendentesOuAgendadosUseCase;
        private readonly ObterHorariosDisponiveisUseCase _obterHorariosDisponiveisUseCase;
        #endregion

        #region Construtores
        public AgendaController(
            CadastrarHorarioUseCase cadastrarHorarioUseCase,
            ListarAgendaMedicoUseCase listarAgendaMedicoUseCase,
            AlterarHorarioUseCase alterarHorarioUseCase,
            AlterarStatusAgendamentoUseCase alterarStatusAgendamentoUseCase,
            ObterHorariosPendentesOuAgendadosUseCase obterHorariosPendentesOuAgendadosUseCase,
            ObterHorariosDisponiveisUseCase obterHorariosDisponiveisUseCase)
        {
            _cadastrarHorarioUseCase = cadastrarHorarioUseCase;
            _listarAgendaMedicoUseCase = listarAgendaMedicoUseCase;
            _alterarHorarioUseCase = alterarHorarioUseCase;
            _alterarStatusAgendamentoUseCase = alterarStatusAgendamentoUseCase;
            _obterHorariosPendentesOuAgendadosUseCase = obterHorariosPendentesOuAgendadosUseCase;
            _obterHorariosDisponiveisUseCase = obterHorariosDisponiveisUseCase;
        }
        #endregion

        #region Actions

        /// <summary>
        /// Endpoint responsável por cadastrar um horário para o médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Agenda/CadastrarHorario
        ///     {
        ///       "dataConsulta": "2025-02-07T19:06:34.777Z",
        ///       "horarioInicio": "string",
        ///       "horarioFim": "string"
        ///     }
        /// </remarks>
        /// <param name="cadAgendaDTO">Objeto com as informações necessárias para cadastrar um horário na agenda.</param>
        /// <returns></returns>
        [HttpPost("CadastrarHorario")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoHorarioCadastrado), 201)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        [AuthorizeMedico]
        public async Task<IActionResult> CadastrarHorario([FromBody] CadAgendaDTO cadAgendaDTO)
        {
            // Extrai o id do médico a partir do token
            if (HttpContext.Items["IdMedico"] is string idMedicoString &&
                int.TryParse(idMedicoString, out int idMedico))
            {
                cadAgendaDTO.IdMedico = idMedico;
            }
            else
            {
                return Forbid("IdMedico não encontrado.");
            }

            RetornoHorarioCadastrado horarioCadastrado = await _cadastrarHorarioUseCase.CadastrarHorario(cadAgendaDTO);
            return CreatedAtAction(nameof(CadastrarHorario), new { id = horarioCadastrado }, horarioCadastrado);
        }

        /// <summary>
        /// Endpoint responsável por listar os agendamentos do médico, confirmados e disponíveis.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     GET /Agenda/Horarios
        /// </remarks>
        /// <returns></returns>
        [HttpGet("Horarios")]
        [AuthorizeMedico]
        public async Task<IActionResult> ListarAgendaMedico()
        {
            if (HttpContext.Items["IdMedico"] is string idMedicoString &&
                int.TryParse(idMedicoString, out int idMedico))
            {
                List<RetornoHorarioCadastrado> agendaMedico = await _listarAgendaMedicoUseCase.ObterHorariosAsync(idMedico);
                return Ok(agendaMedico);
            }
            else
            {
                return Forbid("IdMedico não encontrado no token.");
            }
        }

        /// <summary>
        /// Endpoint responsável por alterar os dados de um horário cadastrado pelo médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     PUT /Agenda/AlterarHorario
        ///     {
        ///         "dataConsulta": "2025-02-07T19:26:57.247Z",
        ///         "horarioInicio": "string",
        ///         "horarioFim": "string"
        ///     }
        /// </remarks>
        /// <param name="agendaDTO">Objeto com as informações para alteração do horário.</param>
        /// <returns></returns>
        [HttpPut("AlterarHorario")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoHorarioCadastrado), 200)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        [ProducesResponseType(typeof(RetornoErroDTO), 404)]
        [AuthorizeMedico]
        public async Task<IActionResult> AlterarHorario([FromBody] CadAgendaDTO agendaDTO)
        {
            if (HttpContext.Items["IdMedico"] is string idMedicoString &&
                int.TryParse(idMedicoString, out int idMedico))
            {
                agendaDTO.IdMedico = idMedico;
            }
            else
            {
                return Forbid("IdMedico não encontrado.");
            }

            var horarioAlterado = await _alterarHorarioUseCase.AlterarHorario(agendaDTO);

            if (horarioAlterado == null)
                return NotFound(new { mensagem = "Horário não encontrado." });

            return Ok(horarioAlterado);
        }

        /// <summary>
        /// Endpoint responsável por listar os horários pendentes ou agendados de um médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     GET /Agenda/HorariosPendentesOuAgendados
        /// </remarks>
        /// <returns></returns>
        [HttpGet("HorariosPendentesOuAgendados")]
        [AuthorizeMedico]
        public async Task<IActionResult> ObterHorariosPendentesOuAgendados()
        {
            if (HttpContext.Items["IdMedico"] is string idMedicoString &&
                int.TryParse(idMedicoString, out int idMedico))
            {
                var horarios = await _obterHorariosPendentesOuAgendadosUseCase.ExecuteAsync(idMedico);
                return Ok(horarios);
            }
            else
            {
                return Forbid("IdMedico não encontrado.");
            }
        }

        /// <summary>
        /// Endpoint responsável por alterar o status de um agendamento.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        ///    PUT /Agenda/AlterarStatusAgendamento
        ///    {
        ///       "idAgendamento": 123,
        ///       "novoStatus": "Agendado",
        ///       "observacoes": "Observação de teste"
        ///    }
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("AlterarStatusAgendamento")]
        [Consumes("application/json")]
        [AuthorizeMedico]
        public async Task<IActionResult> AlterarStatusAgendamento([FromBody] AlterarStatusAgendamentoDTO dto)
        {
            bool sucesso = await _alterarStatusAgendamentoUseCase.ExecuteAsync(dto.IdAgendamento, dto.NovoStatus, dto.Observacoes);
            if (!sucesso)
                return NotFound("Agendamento ou horário não encontrado.");

            return Ok("Status atualizado com sucesso.");
        }

        /// <summary>
        /// Endpoint responsável por listar todos os horários disponíveis para um médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     GET /Agenda/HorariosDisponiveis
        /// </remarks>
        /// <returns></returns>
        [HttpGet("HorariosDisponiveis")]
        [AuthorizeMedico]
        public async Task<IActionResult> ObterHorariosDisponiveis()
        {
            if (HttpContext.Items["IdMedico"] is string idMedicoString &&
                int.TryParse(idMedicoString, out int idMedico))
            {
                var horarios = await _obterHorariosDisponiveisUseCase.ExecuteAsync(idMedico);
                if (horarios == null || horarios.Count == 0)
                    return NotFound("Nenhum horário disponível encontrado para este médico.");

                return Ok(horarios);
            }
            else
            {
                return Forbid("IdMedico não encontrado.");
            }
        }

        #endregion
    }
}
