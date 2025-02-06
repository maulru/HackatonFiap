using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums.Medico;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;

namespace UsuarioAPI.Tests.IntegrationTests.Repositories
{
    public class MedicoRepositoryTests
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly ApplicationDbContext _context;

        public MedicoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

            _context = new ApplicationDbContext(options);
            _medicoRepository = new MedicoRepository(_context);

        }

        [Fact]
        public async Task DeveAdicionarMedicoNoBanco_QuandoMedicoForValido()
        {
            // Arrange
            Medico novoMedico = new Medico
            {
                IdUsuario = "1",
                Especialidade = Especialidades.Cardiologia,
                NumeroCRM = "12345",
                Usuario = new UsuarioBase
                {
                    Nome = "Dr. João",
                    CPF = "000.000.019-1",
                    Tipo = Domain.Enums.TipoUsuario.Medico,
                    Senha = "123456",
                    Email = "joao@email.com"
                }
            };

            Medico medicoAdicionado = await _medicoRepository.Adicionar(novoMedico);

            // Assert
            Assert.NotNull(medicoAdicionado);
            Assert.NotEqual(0, medicoAdicionado.Id);
            Assert.Equal("Dr. João", medicoAdicionado.Usuario.Nome);

            // Verifica se o médico foi salvo corretamente no banco
            var medicoNoBanco = await _context.Set<Medico>().FindAsync(medicoAdicionado.Id);
            Assert.NotNull(medicoNoBanco);
            Assert.Equal("Dr. João", medicoNoBanco.Usuario.Nome);
            Assert.Equal("12345", medicoNoBanco.NumeroCRM);
            Assert.Equal(Especialidades.Cardiologia, medicoNoBanco.Especialidade);
        }

        [Fact]
        public async Task DeveRetornarVerdadeiro_QuandoCRMExistir()
        {
            // Arrange
            string numeroCRM = "12345";

            Medico novoMedico = new Medico
            {
                IdUsuario = "1",
                Especialidade = Especialidades.Cardiologia,
                NumeroCRM = "12345",
                Usuario = new UsuarioBase
                {
                    Nome = "Dr. João",
                    CPF = "000.000.019-1",
                    Tipo = Domain.Enums.TipoUsuario.Medico,
                    Senha = "123456",
                    Email = "joao@email.com"
                }
            };

            _context.Set<Medico>().Add(novoMedico);
            await _context.SaveChangesAsync();

            // Act
            bool resultado = await _medicoRepository.VerificarExistenciaCRM(numeroCRM);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task DeveRetornarFalso_QuandoCRMNaoExistir()
        {
            // Arrange
            string numeroCRM = "12345";

            // Act
            bool resultado = await _medicoRepository.VerificarExistenciaCRM(numeroCRM);

            Assert.False(resultado);
        }

    }
}
