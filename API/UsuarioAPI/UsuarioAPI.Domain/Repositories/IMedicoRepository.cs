using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums.Medico;

namespace UsuarioAPI.Domain.Repositories
{
    public interface IMedicoRepository
    {
        /// <summary>
        /// Responsável por adicionar um novo médico.
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        Task<Medico> Adicionar (Medico medico);

        /// <summary>
        /// Contrato responsável por verificar a existencia de um CRM
        /// </summary>
        /// <param name="numeroCRM"></param>
        /// <returns></returns>
        Task<bool> VerificarExistenciaCRM(string numeroCRM);

        /// <summary>
        /// Contrato responsável por obter a lista de médicos disponíveis por especialidade
        /// </summary>
        /// <param name="filtroEspecialidades"></param>
        /// <returns></returns>
        Task<List<Medico>> ObterMedicosDisponiveisPorEspecialidade(List<Especialidades> filtroEspecialidades);

        /// <summary>
        /// Contrato responsável por obter a lista de médicos disponíveis
        /// </summary>
        /// <returns></returns>
        Task<List<Medico>> ObterMedicosDisponiveis();
    }
}
