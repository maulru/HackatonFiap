namespace UsuarioAPI.Domain.Exceptions
{
    public class UserBaseExceptions : Exception
    {
        public List<string> Erros { get; }

        public UserBaseExceptions(List<string> Erros) : base(string.Join(" ", Erros))
        {
            this.Erros = Erros;
        }
    }
}
