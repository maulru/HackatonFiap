using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Exceptions;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.Services
{
    public class UsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISecurityService _securityRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioValidatorService _usuarioValidator;
        private UserManager<UsuarioBase> _userManager;
        private SignInManager<UsuarioBase> _signInManager;
        private TokenService _tokenService;

        public UsuarioServices(IUsuarioRepository usuarioRepository, ISecurityService securityRepository, 
            IMapper mapper, IUsuarioValidatorService usuarioValidator, SignInManager<UsuarioBase> signInManager,
            TokenService tokenService, UserManager<UsuarioBase> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _securityRepository = securityRepository;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<RetornoUsuarioCadastrado> Cadastrar(UsuarioBase usuario)
        {
            List<string> listaErros = await _usuarioValidator.ObterErrosValidacaoAsync(usuario);

            if (listaErros.Any())
                throw new UserBaseExceptions(listaErros);

            //usuario.Senha = _securityRepository.CriptografarSenha(usuario.Senha);

            IdentityResult resultado = await _usuarioRepository.CadastraAsync(usuario);
            return _mapper.Map<RetornoUsuarioCadastrado>(usuario);
        }

        public async Task<string> Login(LoginDto dto)
        {
            //var password = _securityRepository.CriptografarSenha(dto.Password);
            var resultado = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);


            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedEmail ==
                dto.Email.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

    }
}
