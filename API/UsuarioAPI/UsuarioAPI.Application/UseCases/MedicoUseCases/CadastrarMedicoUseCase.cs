using AutoMapper;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.UseCases.MedicoUseCases
{
    public class CadastrarMedicoUseCase
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly ISecurityService _securityRepository;
        private readonly IMapper _mapper;

        public CadastrarMedicoUseCase(IMedicoRepository medicoRepository, ISecurityService securityRepository, IMapper mapper) 
        {
            _medicoRepository = medicoRepository;
            _securityRepository = securityRepository;
            _mapper = mapper;
        }

        public async Task<RetornoMedicoCadastrado> Cadastrar(Medico medico)
        {
            Medico medicoCadastrado = await _medicoRepository.Adicionar(medico);
            return _mapper.Map<RetornoMedicoCadastrado>(medicoCadastrado);
        }

    }
}
