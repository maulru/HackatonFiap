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
           .ForMember(dest => dest.DataAgendamento, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => Disponibilidade.Pendente))
           .ForMember(dest => dest.Observacoes, opt => opt.Ignore())
           .ForMember(dest => dest.Horario, opt => opt.Ignore());
            CreateMap<Agendamento, RetornoAgendamentoDTO>();
            CreateMap<CancelarAgendamentoDTO, Agendamento>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdAgendamento))
            .ForMember(dest => dest.Observacoes, opt => opt.MapFrom(src => src.Justificativa));
            CreateMap<AlteraAgendamentoDTO, Agendamento>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataAgendamento, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => Disponibilidade.Pendente))
            .ForMember(dest => dest.Observacoes, opt => opt.Ignore())
            .ForMember(dest => dest.Horario, opt => opt.Ignore());
            CreateMap<AlteraAgendamentoDTO, Horario>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.IdMedico, opt => opt.MapFrom(src => src.IdMedico))
                        .ForMember(dest => dest.DataConsulta, opt => opt.MapFrom(src => src.DataConsulta))
                        .ForMember(dest => dest.HorarioInicio, opt => opt.MapFrom(src => src.HorarioInicio))
                        .ForMember(dest => dest.HorarioFim, opt => opt.MapFrom(src => src.HorarioFim))
                        .ForMember(dest => dest.ValorConsulta, opt => opt.MapFrom(src => src.ValorConsulta))
                        .ForMember(dest => dest.Disponibilidade, opt => opt.Ignore()); 


        }
    }
}
