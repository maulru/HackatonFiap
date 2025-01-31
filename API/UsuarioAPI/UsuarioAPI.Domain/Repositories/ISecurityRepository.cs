namespace UsuarioAPI.Domain.Repositories
{
    /// <summary>
    /// Interface de Segurança
    /// </summary>
    public interface ISecurityRepository
    {
        /// <summary>
        /// Contrato responsável por criptografar a senha
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        string CriptografarSenha(string senha);

        /// <summary>
        /// Contrato responsável por validar a senha
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="senhaCriptografada"></param>
        /// <returns></returns>
        bool VerificarSenha(string senha, string senhaCriptografada);
    }
}
