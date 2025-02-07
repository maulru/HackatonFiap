using Moq;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Repositories;

namespace UsuarioAPI.Tests.UnitTests.Application.Services.Usuario
{
    public class UsuarioValidatorServiceTests
    {
        private readonly UsuarioValidatorService _usuarioValidatorService;
        private readonly Mock<IUsuarioRepository> _usuarioRepository;

        public UsuarioValidatorServiceTests()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();

            _usuarioValidatorService = new UsuarioValidatorService(_usuarioRepository.Object);
        }

        [Fact]
        public async Task DeveRetornaListaErro_QuandoNomeNaoForPreenchido()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "",
                Email = "kauabatista545@hotmail.com",
                CPF = "000.000.001-91",
                Senha = "123456",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("O campo 'Nome' é obrigatório!", resultado[0]);
            Assert.Single(resultado);
        }

        [Fact]
        public async Task DeveRetornaListaErro_QuandoEmailNaoForPreenchido()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "",
                CPF = "000.000.001-91",
                Senha = "123456",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("O campo 'E-mail' é obrigatório!", resultado[0]);
            Assert.Single(resultado);
        }

        [Fact]
        public async Task DeveRetornaListaErro_QuandoCPFNaoForPreenchido()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "kauabatista545@hotmail.com",
                CPF = "",
                Senha = "123456",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("O campo 'CPF' é obrigatório!", resultado[0]);
            Assert.Single(resultado);
        }

        [Fact]
        public async Task DeveRetornaListaErro_QuandoSenhaNaoForPreenchido()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "kauabatista545@hotmail.com",
                CPF = "000.000.001-91",
                Senha = "",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("O campo 'Senha' é obrigatório!", resultado[0]);
            Assert.Single(resultado);
        }

        [Fact]
        public async Task DeveRetornaListaErros_QuandoDadosNaoForemPreenchidos()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "",
                Email = "",
                CPF = "",
                Senha = "",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            List<string> erros = new List<string>
            {
                "O campo 'E-mail' é obrigatório!",
                "O campo 'Senha' é obrigatório!",
                "O campo 'Nome' é obrigatório!",           
                "O campo 'CPF' é obrigatório!"
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(false);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(erros, resultado);
            Assert.Equal(erros.Count, resultado.Count);
        }

        [Fact]
        public async Task DeveRetornaListaErro_QuandoCPFJaExistir()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "kauabatista545@hotmail.com",
                CPF = "000.000.001-91",
                Senha = "1234567",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaCPF(usuario.CPF))
                .ReturnsAsync(true);

            _usuarioRepository
                .Setup(repo => repo.VerificarExistenciaEmail(usuario.Email))
                .ReturnsAsync(false);

            // Act
            List<string> resultado = await _usuarioValidatorService.ObterErrosValidacaoAsync(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("O CPF informado já está cadastrado no sistema.", resultado[0]);
            Assert.Single(resultado);
        }
    }
}
