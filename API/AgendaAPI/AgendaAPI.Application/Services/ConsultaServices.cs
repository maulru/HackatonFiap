using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Repositories;
using AutoMapper;

namespace AgendaAPI.Application.Services
{
    public class ConsultaServices
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public ConsultaServices(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<List<RetornoHorarioCadastrado>> ObterHorariosDisponiveisAsync(int idMedico)
        {
            List<Horario> horarios = await _consultaRepository.ObterHorariosDisponiveisAsync(idMedico);
            return _mapper.Map<List<RetornoHorarioCadastrado>>(horarios);
        }

        public async Task<RetornoAgendamentoDTO> CadastrarAgendamentoAsync(CadAgendamentoDTO agendamentoDTO)
        {
            var agendamento = _mapper.Map<Agendamento>(agendamentoDTO);
            agendamento.DataAgendamento = DateTime.Now;
            var agendamentoCadastrado = await _consultaRepository.CadastrarAgendamentoAsync(agendamento);

            return agendamentoCadastrado != null
                ? _mapper.Map<RetornoAgendamentoDTO>(agendamentoCadastrado)
                : null;
        }

        public async Task<bool> CancelarAgendamentoAsync(CancelarAgendamentoDTO cancelarAgendamentoDTO)
        {
            Agendamento agendamento = _mapper.Map<Agendamento>(cancelarAgendamentoDTO);
            return await _consultaRepository.CancelarAgendamentoAsync(agendamento);
        }

    }
}
