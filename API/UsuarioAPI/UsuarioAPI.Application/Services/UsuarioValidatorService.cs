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

            if (await _usuarioRepository.VerificarExistenciaCPF(usuario.CPF))
                listaErros.Add("O CPF informado já está cadastrado no sistema.");

            if (await _usuarioRepository.VerificarExistenciaEmail(usuario.Email))
                listaErros.Add("O E-mail informado já está cadastrado no sistema.");


            return listaErros;
        }
    }
}
