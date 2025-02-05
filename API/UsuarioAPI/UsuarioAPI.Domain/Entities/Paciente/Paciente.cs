using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Domain.Entities.Paciente
{
    /// <summary>
    /// Classe entidade Paciente
    /// </summary>
    public class Paciente
    {
        public int Id { get; set; }
        public required string IdUsuario { get; set; }

        public UsuarioBase Usuario { get; set; }
    }
}
