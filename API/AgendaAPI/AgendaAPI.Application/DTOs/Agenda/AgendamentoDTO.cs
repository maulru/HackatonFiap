using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace AgendaAPI.Application.DTOs.Agenda
{
    public class CadAgendamentoDTO
    {
        [BindNever]
        [SwaggerExclude]
        public int IdPaciente { get; set; }
        public int IdHorario { get; set; }

    }
}
