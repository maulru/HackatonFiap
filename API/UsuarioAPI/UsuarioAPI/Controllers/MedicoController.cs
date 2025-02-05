using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Application.UseCases.MedicoUseCases;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly CadastrarMedicoUseCase _cadastrarMedicoUseCase;

        public MedicoController(CadastrarMedicoUseCase cadastrarMedicoUseCase)
        {
            _cadastrarMedicoUseCase = cadastrarMedicoUseCase;
        }

        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um Médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Paciente/CadastrarMedico
        ///     {
        ///        "nome": "João da Silva",
        ///        "cpf": "123.456.789-00",
        ///        "email": "joao@email.com",
        ///        "senha": "123456",
        ///        "numeroCRM": "1234567",
        ///        "especialidade": 0
        ///     }
        /// 
        /// </remarks>
        /// <param name="medicoDTO">Dados necessários para a requisição</param>
        /// <returns>Confirmação do Cadastro do Médico</returns>
        /// <response code="201">Médico cadastrado com sucesso.</response>
        /// <response code="400">Erro ao cadastrar um Médico</response>
        [HttpPost("CadastrarMedico/")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoMedicoCadastrado), 201)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        public async Task<IActionResult> CadastrarMedico([FromBody] CadMedicoDTO medicoDTO)
        {
            RetornoMedicoCadastrado medicoCadastrado = await _cadastrarMedicoUseCase.Cadastrar(medicoDTO);
            return CreatedAtAction(nameof(CadastrarMedico), new { id = medicoCadastrado.Id }, medicoCadastrado);
        }
    }
}
