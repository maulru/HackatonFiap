using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Repositories;
using AutoMapper;

namespace AgendaAPI.Application.Services
{
    public class AgendaServices
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;

        public AgendaServices(IAgendaRepository agendaRepository, IMapper mapper)
        {
            _agendaRepository = agendaRepository;
            _mapper = mapper;
        }

        public async Task<RetornoHorarioCadastrado> CadastrarHorario(Horario horario)
        {
            await _agendaRepository.CadastraHorarioAsync(horario);
            return _mapper.Map<RetornoHorarioCadastrado>(horario);
        }

    }
}
