namespace UsuarioAPI.Application.DTOs
{
    public class CadPacienteDTO
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
