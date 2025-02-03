namespace UsuarioAPI.Application.DTOs.Base
{
    public class RetornoUsuarioCadastrado
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string? CRM { get; set; }
    }
}
