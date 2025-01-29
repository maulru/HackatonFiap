using Microsoft.AspNetCore.Identity;

namespace UsuarioAPI.Models
{
    public class UsuarioBase : IdentityUser
    {
        public string CPF {  get; set; }
    }
}
