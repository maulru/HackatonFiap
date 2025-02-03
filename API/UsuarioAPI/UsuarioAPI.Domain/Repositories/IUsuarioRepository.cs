using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Domain.Repositories
{
    /// <summary>
    /// Interface do Usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Contrato responsável por adicionar um novo usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<UsuarioBase> Adicionar(UsuarioBase usuario);

        /// <summary>
        /// Método responsável por adicionar um novo médico
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<UsuarioBase> AdicionarMedico(UsuarioBase usuario);

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
