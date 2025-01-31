using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Application.UseCases.PacienteUseCases;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly CadastrarPacienteUseCase _cadastrarPacienteUseCase;

        public PacienteController(CadastrarPacienteUseCase cadastrarPacienteUseCase)
        {
            _cadastrarPacienteUseCase = cadastrarPacienteUseCase;
        }

        [HttpPost("cadastrarPaciente")]
        public async Task<IActionResult> CadastrarPaciente([FromBody] CadPacienteDTO pacienteDTO)
        {
            try
            {
                RetornoPacienteCadastrado pacienteCadastrado = await _cadastrarPacienteUseCase.Executar(pacienteDTO);
                return CreatedAtAction(nameof(CadastrarPaciente), new {id = pacienteCadastrado.Id}, pacienteCadastrado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }
    }
}
