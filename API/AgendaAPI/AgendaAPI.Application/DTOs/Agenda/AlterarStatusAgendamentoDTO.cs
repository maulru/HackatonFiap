using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Application.DTOs.Agenda
{
    public class AlterarStatusAgendamentoDTO
    {
        public int IdAgendamento { get; set; }
        public Disponibilidade NovoStatus { get; set; }
        public string? Observacoes { get; set; }
    }

}
