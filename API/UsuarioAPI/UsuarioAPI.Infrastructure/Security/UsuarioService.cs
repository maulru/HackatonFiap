using Microsoft.AspNetCore.Identity;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Infrastructure.Security
{
    public class UsuarioService
    {
        private UserManager<UsuarioBase> _userManager;
        private SignInManager<UsuarioBase> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(UserManager<UsuarioBase> userManager,
            SignInManager<UsuarioBase> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }



    }
}
