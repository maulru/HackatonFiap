using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.Application.DTOs.Base
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
