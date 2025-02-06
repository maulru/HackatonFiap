using AgendaAPI.Application.DTOs.Agenda;
using AgendaAPI.Domain.Entities.Agenda;
using AutoMapper;

namespace AgendaAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CadAgendaDTO, Horario>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Disponibilidade, opt => opt.Ignore()) 
            .ForMember(dest => dest.Medico, opt => opt.Ignore());

        }
    }
}
