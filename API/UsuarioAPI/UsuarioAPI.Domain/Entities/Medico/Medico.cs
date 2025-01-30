using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Domain.Entities.Medico
{
    /// <summary>
    /// Classe entidade do Médico
    /// </summary>
    public class Medico : UsuarioBase
    {
        /// <summary>
        /// Número do CRM
        /// </summary>
        public required string NumeroCRM { get; set; }
    }
}
