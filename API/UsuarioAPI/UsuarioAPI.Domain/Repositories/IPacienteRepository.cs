using UsuarioAPI.Domain.Entities.Paciente;

namespace UsuarioAPI.Domain.Repositories
{
    /// <summary>
    /// Interface do Paciente
    /// </summary>
    public interface IPacienteRepository
    {
        /// <summary>
        /// Contrato responsável por adicionar um novo paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        Task<Paciente> Adicionar(Paciente paciente);
    }
}
