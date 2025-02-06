using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgendaAPI.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        #region Propriedades
        private readonly IConfiguration _connectionString;
        #endregion

        #region Construtores
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration connectionString) : base(options)
        {
            _connectionString = connectionString;
        }
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        #endregion

    }
}
