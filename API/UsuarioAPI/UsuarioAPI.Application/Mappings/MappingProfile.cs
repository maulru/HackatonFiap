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
            CreateMap<UsuarioDTO, UsuarioBase>()
            .ForMember(dest => dest.Tipo, opt => opt.Ignore());
            CreateMap<UsuarioBase, RetornoUsuarioCadastrado>();
            CreateMap<Medico, RetornoMedicoCadastrado>();

        }
    }
}
