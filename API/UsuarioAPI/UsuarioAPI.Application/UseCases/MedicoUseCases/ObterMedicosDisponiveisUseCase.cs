using AutoMapper;
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

        public async Task ObterMedicosDisponiveis(List<Especialidades> listaEspecialidades)
        {

        }
    }
}
