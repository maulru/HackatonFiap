using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Base;
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

        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um paciente.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Paciente/CadastrarPaciente
        ///     {
        ///        "nome": "João da Silva",
        ///        "cpf": "123.456.789-00",
        ///        "email": "joao@email.com",
        ///        "senha": "123456"
        ///     }
        /// 
        /// </remarks>
        /// <param name="pacienteDTO">Dados necessários para a requisição</param>
        /// <returns>Confirmação do Cadastro do Paciente</returns>
        /// <response code="201">Paciente cadastrado com sucesso.</response>
        /// <response code="400">Erro ao cadastrar um Paciente</response>
        [HttpPost("CadastrarPaciente/")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoPacienteCadastrado), 201)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        public async Task<IActionResult> CadastrarPaciente([FromBody] CadPacienteDTO pacienteDTO)
        {
            RetornoPacienteCadastrado pacienteCadastrado = await _cadastrarPacienteUseCase.Executar(pacienteDTO);
            return CreatedAtAction(nameof(CadastrarPaciente), new { id = pacienteCadastrado.Id }, pacienteCadastrado);
        }
    }
}
