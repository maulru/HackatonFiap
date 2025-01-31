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

        /// <summary>
        /// Contrato responsável por verificar se o CPF Existe. (Retorna True caso exista)
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        Task<bool> VerificarExistenciaCPF(string CPF);

        /// <summary>
        /// Contrato responsável por verificar se o Email Existe. (Retorna True caso exista)
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        Task<bool> VerificarExistenciaEmail(string Email);
    }
}
