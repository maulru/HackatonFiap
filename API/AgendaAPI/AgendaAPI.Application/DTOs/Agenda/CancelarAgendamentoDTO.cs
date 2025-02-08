using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgendaAPI.Application.DTOs.Agenda
{
    public class CancelarAgendamentoDTO
    {
        [BindNever]
        [SwaggerExclude]
        public int IdPaciente {  get; set; }
        public int IdAgendamento { get; set; }

        public string Justificativa { get; set; }
    }
}
