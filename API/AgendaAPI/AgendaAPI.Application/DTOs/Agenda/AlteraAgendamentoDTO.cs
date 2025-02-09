using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAPI.Application.DTOs.Agenda
{
    public class AlteraAgendamentoDTO
    {
        /// <summary>
        /// Id do Agendamento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id do médico
        /// </summary>
        [BindNever]
        [SwaggerExclude]
        public int IdMedico { get; set; }

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
        /// Valor da consulta
        /// </summary>
        public double ValorConsulta { get; set; }
    }
}
