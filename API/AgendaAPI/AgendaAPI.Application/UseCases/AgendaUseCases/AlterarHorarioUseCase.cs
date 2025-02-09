using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;
using AgendaAPI.Domain.Entities.Agenda;
using AutoMapper;
using System.Threading.Tasks;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class AlterarHorarioUseCase
    {
        private readonly AgendaServices _agendaService;
        private readonly IMapper _mapper;

        public AlterarHorarioUseCase(AgendaServices agendaService, IMapper mapper)
        {
            _agendaService = agendaService;
            _mapper = mapper;
        }

        public async Task<RetornoHorarioCadastrado> AlterarHorario(AlteraAgendamentoDTO agendaDTO)
        {
            var horario = _mapper.Map<Horario>(agendaDTO);
            return await _agendaService.AlterarHorario(horario);
        }
    }
}
