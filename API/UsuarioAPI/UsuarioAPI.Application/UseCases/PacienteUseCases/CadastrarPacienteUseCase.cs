using AutoMapper;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Enums;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.UseCases.PacienteUseCases
{
    public class CadastrarPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISecurityService _securityRepository;
        private readonly UsuarioServices _usuarioService;
        private readonly IMapper _mapper;

        public CadastrarPacienteUseCase(IPacienteRepository pacienteRepository, IMapper mapper, 
            ISecurityService securityRepository, UsuarioServices usuarioService)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _securityRepository = securityRepository;
            _usuarioService = usuarioService;
        }

        public async Task<RetornoPacienteCadastrado> Executar(CadPacienteDTO pacienteDTO)
        {
            UsuarioBase usuario = _mapper.Map<UsuarioBase>(pacienteDTO);
            usuario.Tipo = TipoUsuario.Paciente;
            usuario.UserName = pacienteDTO.Email;

            RetornoUsuarioCadastrado usuarioCadastrado = await _usuarioService.Cadastrar(usuario);

            Paciente paciente = new Paciente { IdUsuario = usuarioCadastrado.Id };
            Paciente pacienteCadastrado = await _pacienteRepository.Adicionar(paciente);

            return _mapper.Map<RetornoPacienteCadastrado>(pacienteCadastrado);
        }
    }
}
