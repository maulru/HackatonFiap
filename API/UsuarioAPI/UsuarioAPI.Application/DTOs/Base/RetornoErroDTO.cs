namespace UsuarioAPI.Application.DTOs.Base
{
    public class RetornoErroDTO
    {
        public string mensagem { get; set; }
        public int status { get; set; }
        public List<string> erros { get; set; } = new();

    }
}
