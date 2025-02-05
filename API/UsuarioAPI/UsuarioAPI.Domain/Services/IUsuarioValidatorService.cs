using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Domain.Services
{
    public interface IUsuarioValidatorService
    {
        Task<List<string>> ObterErrosValidacaoAsync(UsuarioBase usuario);
    }
}
