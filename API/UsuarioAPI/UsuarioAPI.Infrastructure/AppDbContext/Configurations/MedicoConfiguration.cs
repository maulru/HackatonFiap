using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuarioAPI.Domain.Entities.Medico;

namespace UsuarioAPI.Infrastructure.AppDbContext.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable(nameof(Medico));

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.IdUsuario).HasColumnName("IdUsuario").HasColumnType("NVARCHAR(450)").IsRequired();
            builder.Property(p => p.NumeroCRM).HasColumnName("NumeroCRM").HasColumnType("VARCHAR(10)").IsRequired();
            builder.Property(p => p.Especialidade).HasColumnName("Especialidade").HasColumnType("INT").IsRequired();


            builder.HasOne(p => p.Usuario)
                .WithOne() 
                .HasForeignKey<Medico>(p => p.IdUsuario) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
