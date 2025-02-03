using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Domain.Entities.Medico
{
    /// <summary>
    /// Classe entidade do Médico
    /// </summary>
    public class Medico 
    {
        public int Id { get; set; }
        /// <summary>
        /// Id do usuário
        /// </summary>
        public required int IdUsuario { get; set; }

        /// <summary>
        /// Número do CRM
        /// </summary>
        public required string NumeroCRM { get; set; }
    }
}
