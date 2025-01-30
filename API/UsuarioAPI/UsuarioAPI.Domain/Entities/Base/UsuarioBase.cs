namespace UsuarioAPI.Domain.Entities.Base
{
    /// <summary>
    /// Classe base Usuário
    /// </summary>
    public class UsuarioBase
    {
        /// <summary>
        /// Id do Usuário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public required string Nome { get; set; }

        /// <summary>
        /// CPF do Usuário
        /// </summary>
        public required string CPF { get; set; }

        /// <summary>
        /// E-mail do Usuário
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public required string Senha { get; set; }
    }
}
