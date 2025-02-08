using AgendaAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.UseCases.MedicoUseCases;
using UsuarioAPI.Domain.Enums.Medico;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class MedicoController : ControllerBase
    {
        #region Propriedades
        private readonly CadastrarMedicoUseCase _cadastrarMedicoUseCase;
        private readonly ObterMedicosDisponiveisUseCase _obterMedicosDisponiveisUseCase;
        #endregion

        #region Construtores
        public MedicoController(CadastrarMedicoUseCase cadastrarMedicoUseCase, 
            ObterMedicosDisponiveisUseCase obterMedicosDisponiveisUseCase)
        {
            _cadastrarMedicoUseCase = cadastrarMedicoUseCase;
            _obterMedicosDisponiveisUseCase = obterMedicosDisponiveisUseCase;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um Médico.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Medico/CadastrarMedico
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

        /// <summary>
        /// Endpoint responsável por realizar a busca de médicos disponíveis
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     GET /Medico/Disponiveis?<paramref name="especialidades"/>
        ///     
        ///     OBS: É possível realizar a busca filtrando especialidades.
        /// 
        /// </remarks>
        /// <param name="especialidades">Especialidades disponíveis para filtrar</param>
        /// <returns>Médicos disponíveis</returns>
        /// <response code="200">Médicos disponíveis retornado.</response>
        /// <response code="400">Erro ao buscar médicos disponíveis.</response>
        [HttpGet("Disponiveis/")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RetornoMedicoDisponivelDTO), 200)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        [AuthorizePaciente]
        public async Task<IActionResult> ObterMedicosDisponiveis([FromQuery] List<Especialidades>? especialidades)
        {
            return Ok(await _obterMedicosDisponiveisUseCase.Executar(especialidades));
        }
        #endregion
    }
}
