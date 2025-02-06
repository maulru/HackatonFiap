using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Application.DTOs.Horario
{
    public class RetornoHorarioCadastrado
    {
        public int Id { get; set; }

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
    }

    
}
