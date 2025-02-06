using AgendaAPI.Domain.Enums.Agenda;

namespace AgendaAPI.Domain.Entities.Agenda
{
    public class Agendamento
    {
        /// <summary>
        /// Id do agendamento
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id do paciente
        /// </summary>
        public int IdPaciente { get; set; }

        /// <summary>
        /// Id do horário selecionado
        /// </summary>
        public int IdHorario { get; set; }

        /// <summary>
        /// Data que foi realizado o agendamento
        /// </summary>
        public DateTime DataAgendamento { get; set; }

        /// <summary>
        /// Situacao do agendamento
        /// </summary>
        public Disponibilidade Situacao { get; set; }

        /// <summary>
        /// Propriedade para armazenar demais informações do agendamento, 
        /// como motivo do cancelamento
        /// </summary>
        public string? Observacoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Horario? Horario { get; set; }
    }
}
