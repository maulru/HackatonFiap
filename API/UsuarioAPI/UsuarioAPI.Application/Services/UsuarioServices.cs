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

        public UsuarioServices(IUsuarioRepository usuarioRepository, ISecurityService securityRepository, 
            IMapper mapper, IUsuarioValidatorService usuarioValidator)
        {
            _usuarioRepository = usuarioRepository;
            _securityRepository = securityRepository;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
        }

        public async Task<RetornoUsuarioCadastrado> Cadastrar(UsuarioBase usuario)
        {
            List<string> listaErros = await _usuarioValidator.ObterErrosValidacaoAsync(usuario);

            if (listaErros.Any())
                throw new UserBaseExceptions(listaErros);

            usuario.Senha = _securityRepository.CriptografarSenha(usuario.Senha);

            IdentityResult resultado = await _usuarioRepository.CadastraAsync(usuario);
            return _mapper.Map<RetornoUsuarioCadastrado>(usuario);
        }

    }
}
