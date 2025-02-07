using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.Services;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthenticationController : ControllerBase
    {
        #region Propriedades
        private UsuarioServices _usuarioService;
        #endregion

        #region Construtores
        public AuthenticationController(UsuarioServices usuarioService)
        {
            _usuarioService = usuarioService;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Endpoint responsável por realizar a autenticação do paciente
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Authentication/Paciente
        ///     {
        ///        "EmailOuCpf": "000.000.001-91",
        ///        "Password": "password"
        ///     }
        /// </remarks>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Paciente")]
        public async Task<IActionResult> LoginPacienteAsync([FromBody] LoginPacienteDto loginDto)
        {
            var token = await _usuarioService.LoginPacienteAsync(loginDto);
            return Ok(token);
        }

        /// <summary>
        /// Endpoint responsável por realizar a autenticação do médico
        /// </summary>
        /// <remarks>
        /// **Exemplo de requisição:**
        /// 
        ///     POST /Authentication/Medico
        ///     {
        ///        "CRM": "102030",
        ///        "Password": "password"
        ///     }
        /// </remarks>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Medico")]
        public async Task<IActionResult> LoginMedicoAsync([FromBody] LoginMedicoDto loginDto)
        {
            var token = await _usuarioService.LoginMedicoAsync(loginDto);
            return Ok(token);
        }
        #endregion
    }
}
