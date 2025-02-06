using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;
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

        public async Task<List<RetornoHorarioCadastrado>> ObterHorariosAsync(int idMedico)
        {
            List<Horario> horario = await _agendaRepository.ObterHorariosAsync(idMedico);

            return _mapper.Map<List<RetornoHorarioCadastrado>>(horario);
        }

        public async Task<RetornoHorarioCadastrado> AlterarHorario(Horario horario)
        {
            Horario horarioAtualizado = await _agendaRepository.AlterarHorarioAsync(horario);
            if (horarioAtualizado == null)
                return null;

            return _mapper.Map<RetornoHorarioCadastrado>(horarioAtualizado);
        }

        public async Task<List<RetornoHorarioCadastrado>> ObterHorariosPendentesOuAgendadosAsync(int idMedico)
        {
            List<Horario> horarios = await _agendaRepository.ObterHorariosPendentesOuAgendadosAsync(idMedico);
            return _mapper.Map<List<RetornoHorarioCadastrado>>(horarios);
        }

        public async Task<bool> AlterarStatusAgendamentoAsync(int idAgendamento, Disponibilidade novoStatus, string observacoes)
        {
            return await _agendaRepository.AlterarStatusAgendamentoAsync(idAgendamento, novoStatus, observacoes);
        }

        public async Task<List<RetornoHorarioCadastrado>> ObterHorariosDisponiveisAsync(int idMedico)
        {
            List<Horario> horarios = await _agendaRepository.ObterHorariosDisponiveisAsync(idMedico);
            return _mapper.Map<List<RetornoHorarioCadastrado>>(horarios);
        }
    }
}
