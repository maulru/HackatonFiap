using AutoMapper;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.DTOs.Paciente;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Entities.Paciente;

namespace UsuarioAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Paciente
            CreateMap<CadPacienteDTO, Paciente>();
            CreateMap<Paciente, RetornoPacienteCadastrado>();
            CreateMap<UsuarioDTO, UsuarioBase>();
            
            CreateMap<UsuarioBase, RetornoUsuarioCadastrado>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
               .ForMember(dest => dest.CRM, opt => opt.Ignore()); // Se CRM não existir em UsuarioBase, ignor
            
            CreateMap<Medico, RetornoMedicoCadastrado>();

        }
    }
}
