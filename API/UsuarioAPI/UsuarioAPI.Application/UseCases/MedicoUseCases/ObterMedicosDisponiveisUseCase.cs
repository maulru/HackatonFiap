using AutoMapper;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums.Medico;
using UsuarioAPI.Domain.Repositories;

namespace UsuarioAPI.Application.UseCases.MedicoUseCases
{
    public class ObterMedicosDisponiveisUseCase
    {
        private readonly IMapper _mapper;
        private readonly IMedicoRepository _medicoRepository;

        public ObterMedicosDisponiveisUseCase(IMapper mapper, IMedicoRepository medicoRepository)
        {
            _mapper = mapper;
            _medicoRepository = medicoRepository;
        }

        public async Task<List<RetornoMedicoDisponivelDTO>> Executar(List<Especialidades> listaEspecialidades)
        {
            List<Medico> medicosDisponiveis = await _medicoRepository.ObterMedicosDisponiveisPorEspecialidade(listaEspecialidades);
            return _mapper.Map<List<RetornoMedicoDisponivelDTO>>(medicosDisponiveis);
        }
    }
}
