using UsuarioAPI.Domain.Repositories;

namespace UsuarioAPI.Infrastructure.Security
{
    public class PasswordService : ISecurityRepository
    {
        public string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaCriptografada);
        }
    }
}
