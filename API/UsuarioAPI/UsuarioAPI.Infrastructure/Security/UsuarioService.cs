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

        public async Task<string> Login(LoginDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);


            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.Email ==
                dto.Email.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

    }
}
