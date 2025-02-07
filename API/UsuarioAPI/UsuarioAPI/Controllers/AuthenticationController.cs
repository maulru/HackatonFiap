using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.Services;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthenticationController : ControllerBase
    {

        private UsuarioServices _usuarioService;

        public AuthenticationController(UsuarioServices usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Login/Paciente")]
        public async Task<IActionResult> LoginPacienteAsync([FromBody] LoginPacienteDto loginDto)
        {
            var token = await _usuarioService.LoginPacienteAsync(loginDto);
            return Ok(token);
        }

        [HttpPost("Login/Medico")]
        public async Task<IActionResult> LoginMedicoAsync([FromBody] LoginMedicoDto loginDto)
        {
            var token = await _usuarioService.LoginMedicoAsync(loginDto);
            return Ok(token);
        }
    }
}
