using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Repositories;

namespace UsuarioAPI.Tests.IntegrationTests.Repositories
{
    public class PacienteRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

            _context = new ApplicationDbContext(options);
            _pacienteRepository = new PacienteRepository(_context);
        }

        [Fact]
        public async Task DeveAdicionarPacienteNoBanco_QuandoPacienteForValido()
        {
            // Arrange
            Paciente novoPaciente = new Paciente
            {
                IdUsuario = "1",
                Usuario = new UsuarioBase
                {
                    Nome = "João",
                    CPF = "000.000.019-1",
                    Tipo = Domain.Enums.TipoUsuario.Paciente,
                    Senha = "123456",
                    Email = "joao@email.com"
                }
            };

            Paciente pacienteAdicionado = await _pacienteRepository.Adicionar(novoPaciente);

            // Assert
            Assert.NotNull(pacienteAdicionado);
            Assert.NotEqual(0, pacienteAdicionado.Id);
            Assert.Equal("João", pacienteAdicionado.Usuario.Nome);

            // Verificação no banco de dados temporario
            var pacienteNoBanco = await _context.Set<Paciente>().FindAsync(pacienteAdicionado.Id);
            Assert.NotNull(pacienteNoBanco);
            Assert.Equal("João", pacienteNoBanco.Usuario.Nome);
        }    
    }   
}
