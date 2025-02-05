using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuarioAPI.Application.DTOs.Base
{
    public class UsuarioDTO
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        //public string? CRM { get; set; }
    }
}
