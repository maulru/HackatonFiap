using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Infrastructure.Security;

namespace UsuarioAPI.Controllers
{
    public class AuthenticationController : ControllerBase
    {

        private UsuarioService _usuarioService;

       [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var token = await _usuarioService.Login(loginDto);
            return Ok(token);
        }
    }
}
