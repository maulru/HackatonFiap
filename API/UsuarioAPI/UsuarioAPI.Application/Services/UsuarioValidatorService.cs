using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.Services
{
    public class UsuarioValidatorService : IUsuarioValidatorService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioValidatorService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<string>> ObterErrosValidacaoAsync(UsuarioBase usuario)
        {
            List<string> listaErros = new List<string>();

            ValidarCamposObrigatorios(usuario, listaErros);
            await ValidacoesNoBancoAsync(usuario, listaErros);

            return listaErros;
        }

        private void ValidarCamposObrigatorios(UsuarioBase usuario, List<string> listaErros)
        {
            var camposObrigatorios = new Dictionary<string, bool>
            {
                { "E-mail", string.IsNullOrWhiteSpace(usuario.Email) },
                { "Senha", string.IsNullOrWhiteSpace(usuario.Senha) },
                { "Nome", string.IsNullOrWhiteSpace(usuario.Nome) },
                { "CPF", string.IsNullOrWhiteSpace(usuario.CPF) }

            };

            foreach (var campo in camposObrigatorios)
            {
                if (campo.Value)
                    listaErros.Add($"O campo '{campo.Key}' é obrigatório!");
            }
        }

        private async Task ValidacoesNoBancoAsync(UsuarioBase usuario, List<string> listaErros)
        {
            if (await _usuarioRepository.VerificarExistenciaCPF(usuario.CPF))
                listaErros.Add("O CPF informado já está cadastrado no sistema.");

            if (await _usuarioRepository.VerificarExistenciaEmail(usuario.Email))
                listaErros.Add("O E-mail informado já está cadastrado no sistema.");
        }
    }
}
