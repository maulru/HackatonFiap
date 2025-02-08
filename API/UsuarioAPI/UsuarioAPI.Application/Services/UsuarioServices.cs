using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Enums;
using UsuarioAPI.Domain.Exceptions;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.Services
{
    public class UsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioValidatorService _usuarioValidator;
        private UserManager<UsuarioBase> _userManager;
        private SignInManager<UsuarioBase> _signInManager;
        private TokenService _tokenService;

        public UsuarioServices(IUsuarioRepository usuarioRepository, 
            IMapper mapper, IUsuarioValidatorService usuarioValidator, SignInManager<UsuarioBase> signInManager,
            TokenService tokenService, UserManager<UsuarioBase> userManager, IMedicoRepository medicoRepository,
            IPacienteRepository pacienteRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _usuarioValidator = usuarioValidator;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
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

        public async Task<string> LoginMedicoAsync(LoginMedicoDto dto)
        {
       
            var medicoEntity = await _medicoRepository.GetByCRMAsync(dto.CRM);
            if (medicoEntity == null)
                throw new ApplicationException("Médico não encontrado!");

            var medico = _mapper.Map<Medico>(medicoEntity);

            var usuario = await _userManager.FindByIdAsync(medico.IdUsuario);
            if (usuario == null || usuario.Tipo != TipoUsuario.Medico)
                throw new ApplicationException("Usuário não encontrado ou não autorizado!");

            var resultado = await _signInManager.PasswordSignInAsync(usuario, dto.Password, false, false);
            if (!resultado.Succeeded)
                throw new ApplicationException("Usuário não autenticado!");

            return _tokenService.GenerateMedicoToken(medico);
        }

        public async Task<string> LoginPacienteAsync(LoginPacienteDto dto)
        {
            var usuario = await _userManager.FindByEmailAsync(dto.EmailOuCPF)
                          ?? await _userManager.Users.FirstOrDefaultAsync(u => u.CPF == dto.EmailOuCPF);

            if (usuario == null || usuario.Tipo != TipoUsuario.Paciente)
                throw new ApplicationException("Usuário não encontrado ou não autorizado!");

            var pacienteEntity = await _pacienteRepository.GetByIdUsuarioAsync(usuario.Id);
            if (pacienteEntity == null)
                throw new ApplicationException("Paciente não encontrado!");

            var paciente = _mapper.Map<Paciente>(pacienteEntity);

            var resultado = await _signInManager.PasswordSignInAsync(usuario, dto.Password, false, false);
            if (!resultado.Succeeded)
                throw new ApplicationException("Usuário não autenticado!");

            return _tokenService.GeneratePacienteToken(paciente);
        }

    }
}
