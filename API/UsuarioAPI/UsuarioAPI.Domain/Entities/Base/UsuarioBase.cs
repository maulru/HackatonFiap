using Microsoft.AspNetCore.Identity;

namespace UsuarioAPI.Domain.Entities.Base
{
    /// <summary>
    /// Classe base Usuário
    /// </summary>
    public class UsuarioBase : IdentityUser
    {

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public required string Nome { get; set; }

        /// <summary>
        /// CPF do Usuário
        /// </summary>
        public required string CPF { get; set; }


        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public required string Senha { get; set; }
        
        /// <summary>
        /// Tipo do usuário
        /// </summary>
        public required string Tipo { get; set; }
        
    }
}
