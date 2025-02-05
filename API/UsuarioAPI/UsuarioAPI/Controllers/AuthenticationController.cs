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

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var token = await _usuarioService.Login(loginDto);
            return Ok(token);
        }
    }
}
