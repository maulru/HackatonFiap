using UsuarioAPI.Domain.Enums.Medico;

namespace UsuarioAPI.Application.DTOs.Medico
{
    public class RetornoMedicoDisponivelDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Especialidades Especialidade { get; set; }
        public string? NumeroCRM { get; set; }
    }
}
