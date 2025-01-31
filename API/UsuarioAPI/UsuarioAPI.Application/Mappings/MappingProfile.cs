using AutoMapper;
using UsuarioAPI.Application.DTOs;
using UsuarioAPI.Application.DTOs.Paciente;
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
        }
    }
}
