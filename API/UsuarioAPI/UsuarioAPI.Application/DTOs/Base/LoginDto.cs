using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.Application.DTOs.Base
{
    public class LoginPacienteDto
    {
        [Required]
        public string EmailOuCPF { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginMedicoDto
    {
        [Required]
        public string CRM { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
