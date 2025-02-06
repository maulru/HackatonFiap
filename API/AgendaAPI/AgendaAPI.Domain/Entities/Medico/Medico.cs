using AgendaAPI.Domain.Enums.Medico;

namespace AgendaAPI.Domain.Entities
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
        public required string IdUsuario { get; set; }

        /// <summary>
        /// Número do CRM
        /// </summary>
        public required string NumeroCRM { get; set; }

        public required Especialidades Especialidade { get; set; }


    }
}
