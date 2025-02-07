using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Application.Services;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;
using AutoMapper;

namespace AgendaAPI.Application.UseCases.AgendaUseCases
{
    public class CadastrarHorarioUseCase
    {

        private readonly IMapper _mapper;
        private AgendaServices _agendaService;


        public CadastrarHorarioUseCase(IMapper mapper, AgendaServices agendaServices)
        {
            _mapper = mapper;
            _agendaService = agendaServices;
        }

        public async Task<RetornoHorarioCadastrado> CadastrarHorario(CadAgendaDTO cadAgendaDTO)
        {
            Horario horario = _mapper.Map<Horario>(cadAgendaDTO);
            horario.Disponibilidade = Disponibilidade.Disponivel;

            RetornoHorarioCadastrado retornoHorarioCadastrado = await _agendaService.CadastrarHorario(horario);

            return _mapper.Map<RetornoHorarioCadastrado>(retornoHorarioCadastrado);
        }

    }
}
