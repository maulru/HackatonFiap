using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgendaAPI.Application.DTOs.Agenda
{
    public class CadAgendaDTO
    {
        [BindNever]
        [SwaggerExclude]
        public int IdMedico {  get; set; }

        public DateTime DataConsulta { get; set; }
        /// <summary>
        /// Horario inicio para a consulta
        /// </summary>
        public TimeSpan HorarioInicio { get; set; }
        /// <summary>
        /// Horario final para a consulta
        /// </summary>
        public TimeSpan HorarioFim { get; set; }
    }
}
