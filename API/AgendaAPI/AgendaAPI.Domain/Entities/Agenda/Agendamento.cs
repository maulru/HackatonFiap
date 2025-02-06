namespace AgendaAPI.Domain.Entities.Agenda
{
    public class Agendamento
    {
        public int Id { get; set; }

        public int IdPaciente { get; set; }

        public int IdHorario { get; set; }

        public DateTime DataAgendamento { get; set; }
    }
}
