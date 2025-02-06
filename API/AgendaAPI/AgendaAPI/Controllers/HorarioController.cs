using AgendaAPI.Application.UseCases.HorarioUseCases;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    public class HorarioController : ControllerBase
    {

        private readonly ListarMedicoUseCase _listarMedicoUseCase;
        private readonly ListarHorariosMedicoUseCase _listarHorariosMedicoUseCase;
        private readonly CadastrarAgendamentoUseCase _cadastrarAgendamentoUseCase;
        private readonly CancelarAgendamentoUseCase _cancelarAgendamentoUseCase;
      

        public HorarioController(ListarMedicoUseCase listarMedicoUseCase,
            ListarHorariosMedicoUseCase listarHorariosMedicoUseCase,
            CadastrarAgendamentoUseCase cadastrarAgendamentoUseCase,
            CancelarAgendamentoUseCase cancelarAgendamentoUseCase) 
        {
            _listarMedicoUseCase = listarMedicoUseCase;
            _listarHorariosMedicoUseCase = listarHorariosMedicoUseCase;
            _cadastrarAgendamentoUseCase = cadastrarAgendamentoUseCase;
            _cancelarAgendamentoUseCase = cancelarAgendamentoUseCase;
        }


        /*
         * ListarMedicos (por especialidade)
         * ListaHorariosMedico
         * Cadastrar Agendamento
         * Cancelar Agendamento
         * 
         */


    }
}
