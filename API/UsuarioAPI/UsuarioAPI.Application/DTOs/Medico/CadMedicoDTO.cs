using UsuarioAPI.Domain.Enums.Medico;

namespace UsuarioAPI.Application.DTOs.Medico
{
    public class CadMedicoDTO
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string NumeroCRM { get; set; }
        public required Especialidades Especialidade { get; set; }
    }
}
