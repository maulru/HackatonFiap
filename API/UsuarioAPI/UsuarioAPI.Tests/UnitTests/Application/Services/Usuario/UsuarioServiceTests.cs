using AutoMapper;
using Moq;
using UsuarioAPI.Application.Mappings;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Tests.UnitTests.Application.Services.Usuario
{
    public class UsuarioServiceTests
    {
        private readonly UsuarioServices _usuarioServices;
        private readonly Mock<IUsuarioRepository> _usuarioRepository;
        private readonly Mock<IUsuarioValidatorService> _usuarioValidator;
        private readonly IMapper _mapper;

        public UsuarioServiceTests()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioValidator = new Mock<IUsuarioValidatorService>();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            //_usuarioServices = new UsuarioServices(_usuarioRepository.Object, )
        }

    }
}
