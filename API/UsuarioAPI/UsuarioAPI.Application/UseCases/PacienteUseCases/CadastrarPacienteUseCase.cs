using AutoMapper;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Repositories;

namespace UsuarioAPI.Application.UseCases.PacienteUseCases
{
    public class CadastrarPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IMapper _mapper;

        public CadastrarPacienteUseCase(IPacienteRepository pacienteRepository, IMapper mapper, ISecurityRepository securityRepository)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _securityRepository = securityRepository;
        }

        public async Task<RetornoPacienteCadastrado> Executar(CadPacienteDTO pacienteDTO)
        {
            Paciente paciente = _mapper.Map<Paciente>(pacienteDTO);
            paciente.Senha = _securityRepository.CriptografarSenha(paciente.Senha);

            Paciente pacienteCadastrado = await _pacienteRepository.Adicionar(paciente);
            return _mapper.Map<RetornoPacienteCadastrado>(pacienteCadastrado);
        }
    }
}
