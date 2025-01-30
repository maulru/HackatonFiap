using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Entities.Paciente;

namespace UsuarioAPI.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        #region Propriedades
        private readonly IConfiguration _connectionString;
        #endregion

        #region Construtores
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration connectionString) : base(options)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region DbSets
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        #endregion

        #region Métodos Override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString.GetConnectionString("ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        #endregion
    }
}
