using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Domain.Entities.Agenda
{
    public class Horario
    {
        /// <summary>
        /// Id do horário
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id do médico
        /// </summary>
        public int IdMedico { get; set; }
        /// <summary>
        /// Data disponível para a consulta
        /// </summary>
        public DateTime DataConsulta { get; set; }
        /// <summary>
        /// Horario inicio para a consulta
        /// </summary>
        public TimeSpan HorarioInicio { get; set; }
        /// <summary>
        /// Horario final para a consulta
        /// </summary>
        public TimeSpan HorarioFim { get; set; }
        /// <summary>
        /// Status de disponibilidade
        /// </summary>
        public Disponibilidade Disponibilidade { get; set; }

        public Medico Medico { get; set; }
    }
}
