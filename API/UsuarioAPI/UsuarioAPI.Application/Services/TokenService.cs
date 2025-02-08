using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Entities.Paciente;

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

        public string GenerateMedicoToken(Medico medico)
        {
           
            Claim[] claims = new Claim[]
            {
            new Claim("email", medico.Usuario.Email),
            new Claim("id", medico.Usuario.Id),
            new Claim("tipo", medico.Usuario.Tipo.ToString()),
            new Claim("IdMedico", medico.Id.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
            var signinCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(20),
                claims: claims,
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GeneratePacienteToken(Paciente paciente)
        {

            Claim[] claims = new Claim[]
            {
            new Claim("email", paciente.Usuario.Email),
            new Claim("id", paciente.Usuario.Id),
            new Claim("tipo", paciente.Usuario.Tipo.ToString()),
            new Claim("IdPaciente", paciente.Id.ToString()) 
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
            var signinCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(20),
                claims: claims,
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
