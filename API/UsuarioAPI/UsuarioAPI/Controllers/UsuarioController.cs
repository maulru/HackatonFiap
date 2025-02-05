using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.UseCases.MedicoUseCases;
using UsuarioAPI.Application.UseCases.PacienteUseCases;
using UsuarioAPI.Domain.Entities.Medico;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController: ControllerBase
    {
        private readonly CadastrarUsuarioUseCase _cadastrarUsuarioUseCase;
        private readonly CadastrarMedicoUseCase _cadastrarMedicoUseCase;

        public UsuarioController(CadastrarUsuarioUseCase cadastrarUsuarioUseCase, CadastrarMedicoUseCase cadastrarMedicoUseCase)
        {
            _cadastrarUsuarioUseCase = cadastrarUsuarioUseCase;
            _cadastrarMedicoUseCase = cadastrarMedicoUseCase;
        }

        /// <summary>
        /// Endpoint responsável por realizar o cadastro de um usuario.
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Usuario/CadastrarUsuario
        ///     {
        ///        "nome": "João da Silva",
        ///        "cpf": "123.456.789-00",
        ///        "email": "joao@email.com",
        ///        "senha": "123456",
        ///        "CRM": "101010"
        ///     }
        /// 
        /// </remarks>
        /// <param name="usuarioDTO">Dados necessários para a requisição</param>
        /// <returns>Confirmação do Cadastro do Usuario</returns>
        /// <response code="201">Usuario cadastrado com sucesso.</response>
        /// <response code="400">Erro ao cadastrar um Usuario</response>
        [HttpPost("CadastrarUsuario/")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RetornoUsuarioCadastrado), 201)]
        [ProducesResponseType(typeof(RetornoErroDTO), 400)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            /*
            RetornoUsuarioCadastrado usuarioCadastrado = await _cadastrarUsuarioUseCase.Executar(usuarioBase);
            
            if (!String.IsNullOrEmpty(usuarioDTO.CRM) && !String.IsNullOrEmpty(usuarioCadastrado.Id))
            {
                usuarioCadastrado.CRM = usuarioDTO.CRM;
                AdicionarMedicoDTO medicoDTO = new AdicionarMedicoDTO()
                {
                    Id = usuarioCadastrado.Id,
                    CRM = usuarioCadastrado.CRM
                };

                AdicionarMedico(medicoDTO);
            }
            return CreatedAtAction(nameof(CadastrarUsuario), new { id = usuarioCadastrado.Id }, usuarioCadastrado);
            */

            return Ok();
        }

        
        [HttpPost("AdicionarMedico/")]
        public async Task<IActionResult> AdicionarMedico([FromBody] AdicionarMedicoDTO model)
        {
            try
            {
                Medico medico = new Medico()
                {
                    IdUsuario = model.Id,
                    NumeroCRM = model.CRM,
                    Especialidade = 0
                };

                //await _cadastrarMedicoUseCase.Cadastrar(medico);
                return Ok(model);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
