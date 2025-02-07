using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsuarioAPI.Application.Mappings;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Exceptions;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsuarioAPI.Tests.UnitTests.Application.Services.Usuario
{
    public class UsuarioServiceTests
    {
        private readonly UsuarioServices _usuarioServices;

        private readonly UserManager<UsuarioBase> _userManager;
        private readonly SignInManager<UsuarioBase> _signInManager;
        private readonly TokenService _tokenService;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IUsuarioValidatorService> _usuarioValidatorMock;
        private readonly IMapper _mapper;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioValidatorMock = new Mock<IUsuarioValidatorService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _userManager = CreateUserManager();
            _signInManager = CreateSignInManager(_userManager);
            _tokenService = CreateTokenService();

            _usuarioServices = new UsuarioServices(
                _usuarioRepositoryMock.Object,
                _mapper,
                _usuarioValidatorMock.Object,
                _signInManager,
                _tokenService,
                _userManager
            );
        }

        [Fact]
        public async Task DeveCadastrarUsuario_QuandoDadosValido()
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

            _usuarioValidatorMock
                .Setup(v => v.ObterErrosValidacaoAsync(usuario))
                .ReturnsAsync(new List<string>());

            _usuarioRepositoryMock
                .Setup(repo => repo.CadastraAsync(usuario))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var resultado = await _usuarioServices.Cadastrar(usuario);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuario.Nome, resultado.Nome);
            Assert.Equal(usuario.Email, resultado.Email);
        }

        [Fact]
        public async Task DeveLancarExcecao_QuandoDadosForInvalido()
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

            List<string> errors = new List<string>() { "O campo 'nome' é obrigatório!" };

            _usuarioValidatorMock
            .Setup(v => v.ObterErrosValidacaoAsync(usuario))
            .ReturnsAsync(errors);

            // Act
            var exception = await Assert.ThrowsAsync<UserBaseExceptions>(
                async () => await _usuarioServices.Cadastrar(usuario));

            // Assert
            Assert.Equal(errors, exception.Erros);
        }

        [Fact]
        public async Task DeveLancarExcecao_QuandoCadastroFalhar()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "kauabatista545@hotmail.com",
                CPF = "000.000.001-91",
                Senha = "123456",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            _usuarioValidatorMock
            .Setup(v => v.ObterErrosValidacaoAsync(usuario))
            .ReturnsAsync(new List<string>());

            _usuarioRepositoryMock
            .Setup(repo => repo.CadastraAsync(It.IsAny<UsuarioBase>()))
            .ThrowsAsync(new UserBaseExceptions(new List<string> { "Erro ao cadastrar usuário." }));

            // Act
            var exception = await Assert.ThrowsAsync<UserBaseExceptions>(
            async () => await _usuarioServices.Cadastrar(usuario));

            Assert.Equal("Erro ao cadastrar usuário.", exception.Message);
        }

        [Fact]
        public async Task DeveLancarExcecao_QuandoUsuarioJaExiste()
        {
            // Arrange
            UsuarioBase usuario = new UsuarioBase
            {
                Nome = "Antonio Kauã",
                Email = "kauabatista545@hotmail.com",
                CPF = "000.000.001-91",
                Senha = "123456",
                Tipo = Domain.Enums.TipoUsuario.Paciente
            };

            List<string> errors = new List<string>() { "O CPF informado já está cadastrado no sistema." };

            _usuarioValidatorMock
            .Setup(v => v.ObterErrosValidacaoAsync(usuario))
            .ReturnsAsync(errors);

            // Act
            var exception = await Assert.ThrowsAsync<UserBaseExceptions>(
                async () => await _usuarioServices.Cadastrar(usuario));

            // Assert
            Assert.Equal(errors, exception.Erros);
        }


        #region Métodos Auxiliares

        private TokenService CreateTokenService()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c["Jwt:Key"]).Returns("X");
            configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("Y");

            return new TokenService(configurationMock.Object);
        }

        private UserManager<UsuarioBase> CreateUserManager()
        {
            var store = new Mock<IUserStore<UsuarioBase>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var passwordHasher = new Mock<IPasswordHasher<UsuarioBase>>();
            var userValidators = new List<IUserValidator<UsuarioBase>> { new Mock<IUserValidator<UsuarioBase>>().Object };
            var passwordValidators = new List<IPasswordValidator<UsuarioBase>> { new Mock<IPasswordValidator<UsuarioBase>>().Object };
            var lookupNormalizer = new Mock<ILookupNormalizer>();
            var identityErrorDescriber = new Mock<IdentityErrorDescriber>();
            var services = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<UsuarioBase>>>();

            return new UserManager<UsuarioBase>(
                store.Object,
                options.Object,
                passwordHasher.Object,
                userValidators,
                passwordValidators,
                lookupNormalizer.Object,
                identityErrorDescriber.Object,
                services.Object,
                logger.Object
            );
        }

        private SignInManager<UsuarioBase> CreateSignInManager(UserManager<UsuarioBase> userManager)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<UsuarioBase>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var logger = new Mock<ILogger<SignInManager<UsuarioBase>>>();
            var schemeProvider = new Mock<IAuthenticationSchemeProvider>();

            return new SignInManager<UsuarioBase>(
                userManager,
                contextAccessor.Object,
                claimsFactory.Object,
                options.Object,
                logger.Object,
                schemeProvider.Object
            );
        }

        #endregion
    }
}
