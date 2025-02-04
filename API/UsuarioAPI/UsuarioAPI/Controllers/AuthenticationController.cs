using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Infrastructure.Repositories;
using UsuarioAPI.Infrastructure.Security;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthenticationController : ControllerBase
    {

        private UsuarioRepository _usuarioRepository;

        public AuthenticationController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var token = await _usuarioRepository.Login(loginDto);
            return Ok(token);
        }
    }
}
