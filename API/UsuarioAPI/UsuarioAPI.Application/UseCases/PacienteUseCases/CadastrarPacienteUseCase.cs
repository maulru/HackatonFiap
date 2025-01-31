using AutoMapper;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Exceptions;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.UseCases.PacienteUseCases
{
    public class CadastrarPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISecurityService _securityRepository;
        private readonly IMapper _mapper;

        public CadastrarPacienteUseCase(IPacienteRepository pacienteRepository, IMapper mapper, ISecurityService securityRepository)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _securityRepository = securityRepository;
        }

        public async Task<RetornoPacienteCadastrado> Executar(CadPacienteDTO pacienteDTO)
        {
            Paciente paciente = _mapper.Map<Paciente>(pacienteDTO);
            List<string> listaErros = await ObterErrosValidacaoAsync(paciente);

            if (listaErros.Any())
                throw new UserBaseExceptions(listaErros);

            paciente.Senha = _securityRepository.CriptografarSenha(paciente.Senha);

            Paciente pacienteCadastrado = await _pacienteRepository.Adicionar(paciente);
            return _mapper.Map<RetornoPacienteCadastrado>(pacienteCadastrado);
        }

        private async Task<List<string>> ObterErrosValidacaoAsync(Paciente paciente)
        {
            List<string> listaErros = new List<string>();

            if (await _pacienteRepository.VerificarExistenciaCPF(paciente.CPF))
                listaErros.Add("O CPF informado já está cadastrado no sistema.");

            if (await _pacienteRepository.VerificarExistenciaEmail(paciente.Email))
                listaErros.Add("O E-mail informado já está cadastrado no sistema.");


            return listaErros;
        }
    }
}
