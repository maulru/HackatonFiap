using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Base;
using AgendaAPI.Application.UseCases.AgendaUseCases;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AgendaController : ControllerBase
    {

        private readonly CadastrarHorarioUseCase _cadastrarHorarioUseCase;

        public AgendaController(CadastrarHorarioUseCase cadastrarHorarioUseCase)
        {
            _cadastrarHorarioUseCase = cadastrarHorarioUseCase;
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

    }
}
