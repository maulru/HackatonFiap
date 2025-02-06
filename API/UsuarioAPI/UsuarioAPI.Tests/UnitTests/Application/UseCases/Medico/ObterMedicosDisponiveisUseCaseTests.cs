using AutoMapper;
using Moq;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.Mappings;
using UsuarioAPI.Application.UseCases.MedicoUseCases;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums.Medico;
using UsuarioAPI.Domain.Repositories;

namespace UsuarioAPI.Tests.UnitTests.Application
{

    public class ObterMedicosDisponiveisUseCaseTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ObterMedicosDisponiveisUseCase _useCase;

        public ObterMedicosDisponiveisUseCaseTests()
        {
            _medicoRepositoryMock = new Mock<IMedicoRepository>();
            
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _useCase = new ObterMedicosDisponiveisUseCase(_mapper, _medicoRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveRetornarListaMedicosDTO_QuandoEspecialidadeForInformada()
        {
            // Arrange
            List<Especialidades> especialidades = new List<Especialidades> { Especialidades.Cardiologia };

            List<Medico> medicosMock = new List<Medico>
            {
                new Medico
                {
                    Id = 1,
                    IdUsuario = "1",
                    Especialidade = Especialidades.Cardiologia,
                    NumeroCRM = "12345",
                    Usuario = new UsuarioBase
                    {
                        Nome = "Dr. João",
                        CPF = "000.000.019-1",
                        Tipo = Domain.Enums.TipoUsuario.Medico,
                        Email = "joao@emai.com",
                        Senha = "123456"
                    }
                }
            };

            var dtosMock = new List<RetornoMedicoDisponivelDTO>
            {
                new RetornoMedicoDisponivelDTO { Id = 1, Nome = "Dr. João", Especialidade = Especialidades.Cardiologia, NumeroCRM = "12345" }
            };

            _medicoRepositoryMock.Setup(repo => repo.ObterMedicosDisponiveisPorEspecialidade(especialidades))
            .ReturnsAsync(medicosMock);


            // Act
            List<RetornoMedicoDisponivelDTO> resultado = await _useCase.Executar(especialidades);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("Dr. João", resultado[0].Nome);
        }

        [Fact]
        public async Task DeveRetornarListaVazia_QuandoNaoHouverMedicos()
        {
            // Arrange
            List<Especialidades> especialidades = new List<Especialidades> { Especialidades.Ortopedia };
            
            _medicoRepositoryMock.Setup(repo => repo.ObterMedicosDisponiveisPorEspecialidade(especialidades))
            .ReturnsAsync(new List<Medico>());

            // Act
            List<RetornoMedicoDisponivelDTO> resultado = await _useCase.Executar(especialidades);

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task DeveRetornarMedicosComEspecialidadeFiltrada_QuandoEspecialidadeForInformada()
        {
            // Arrange
            List<Especialidades> especialidadesFiltro = new List<Especialidades> { Especialidades.Cardiologia };

            List<Medico> medicosMock = new List<Medico>
            {
                new Medico
                {
                    Id = 1,
                    IdUsuario = "1",
                    Especialidade = Especialidades.Cardiologia,
                    NumeroCRM = "12345",
                    Usuario = new UsuarioBase
                    {
                        Nome = "Dr. João",
                        CPF = "000.000.019-1",
                        Tipo = Domain.Enums.TipoUsuario.Medico,
                        Email = "joao@emai.com",
                        Senha = "123456"
                    }
                },
                new Medico
                {
                    Id = 2,
                    IdUsuario = "2",
                    Especialidade = Especialidades.Ortopedia, // Não deve ser retornado
                    NumeroCRM = "67890",
                    Usuario = new UsuarioBase
                    {
                        Nome = "Dr. Carlos",
                        CPF = "000.000.019-2",
                        Tipo = Domain.Enums.TipoUsuario.Medico,
                        Email = "carlos@emai.com",
                        Senha = "123456"
                    }
                }
            };

            List<RetornoMedicoDisponivelDTO> dtosMock = new List<RetornoMedicoDisponivelDTO>
            {
                new RetornoMedicoDisponivelDTO
                {
                    Id = 1,
                    Nome = "Dr. João",
                    Especialidade = Especialidades.Cardiologia,
                    NumeroCRM = "12345"
                }
            };

            _medicoRepositoryMock
                .Setup(repo => repo.ObterMedicosDisponiveisPorEspecialidade(especialidadesFiltro))
                .ReturnsAsync(medicosMock.FindAll(m => especialidadesFiltro.Contains(m.Especialidade)));

            //Act
            List<RetornoMedicoDisponivelDTO> resultado = await _useCase.Executar(especialidadesFiltro);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado); // Deve retornar apenas um médico
            Assert.Equal("Dr. João", resultado[0].Nome); // Verifica se é o médico correto
            Assert.Equal(Especialidades.Cardiologia, resultado[0].Especialidade); // Verifica se é a especialidade correta

        }

        [Fact]
        public async Task DeveRetornarTodosOsMedicos_QuandoEspecialidadeNAOForInformada()
        {
            // Arrange
            List<Especialidades> especialidades = new List<Especialidades>();

            List<Medico> medicosMock = new List<Medico>
            {
                new Medico
                {
                    Id = 1,
                    IdUsuario = "1",
                    Especialidade = Especialidades.Cardiologia,
                    NumeroCRM = "12345",
                    Usuario = new UsuarioBase
                    {
                        Nome = "Dr. João",
                        CPF = "000.000.019-1",
                        Tipo = Domain.Enums.TipoUsuario.Medico,
                        Email = "joao@emai.com",
                        Senha = "123456"
                    }
                },
                new Medico
                {
                    Id = 2,
                    IdUsuario = "2",
                    Especialidade = Especialidades.Ortopedia,
                    NumeroCRM = "67890",
                    Usuario = new UsuarioBase
                    {
                        Nome = "Dr. Carlos",
                        CPF = "000.000.019-2",
                        Tipo = Domain.Enums.TipoUsuario.Medico,
                        Email = "carlos@emai.com",
                        Senha = "123456"
                    }
                }
            };

            _medicoRepositoryMock.Setup(repo => repo.ObterMedicosDisponiveisPorEspecialidade(especialidades))
            .ReturnsAsync(medicosMock);

            // Act
            List<RetornoMedicoDisponivelDTO> resultado = await _useCase.Executar(especialidades);

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.NotNull(resultado);

        }

    }
}
