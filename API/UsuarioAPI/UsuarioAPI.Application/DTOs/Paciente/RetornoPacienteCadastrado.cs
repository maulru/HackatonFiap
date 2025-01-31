namespace UsuarioAPI.Application.DTOs.Paciente
{
    public class RetornoPacienteCadastrado
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
