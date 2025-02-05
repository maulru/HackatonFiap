using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Application.Services
{
    public class TokenService
    {

        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UsuarioBase usuario)
        {
            Claim[] claims = new Claim[] {
                new Claim("email", usuario.Email),
                new Claim("id",usuario.Id),
                new Claim("tipo",usuario.Tipo.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

            var signinCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
