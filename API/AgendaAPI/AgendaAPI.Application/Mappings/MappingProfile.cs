using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Application.DTOs.Horario;
using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;
using AutoMapper;

namespace AgendaAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CadAgendaDTO, Horario>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Disponibilidade, opt => opt.Ignore());
            CreateMap<Horario, RetornoHorarioCadastrado>();
            CreateMap<CadAgendamentoDTO, Agendamento>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()) 
           .ForMember(dest => dest.DataAgendamento, opt => opt.MapFrom(src => DateTime.UtcNow)) 
           .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => Disponibilidade.Pendente)) 
           .ForMember(dest => dest.Observacoes, opt => opt.Ignore()) 
           .ForMember(dest => dest.Horario, opt => opt.Ignore());
            CreateMap<Agendamento, RetornoAgendamentoDTO>();

        }
    }
}
