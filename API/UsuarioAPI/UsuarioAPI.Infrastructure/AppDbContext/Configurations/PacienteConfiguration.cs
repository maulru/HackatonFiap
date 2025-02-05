using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuarioAPI.Domain.Entities.Paciente;

namespace UsuarioAPI.Infrastructure.AppDbContext.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable(nameof(Paciente));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.IdUsuario).HasColumnName("IdUsuario").HasColumnType("NVARCHAR(450)").IsRequired();

            // Criando relacionamento com UsuarioBase
            builder.HasOne(p => p.Usuario)
                .WithOne() // Paciente tem um usuário
                .HasForeignKey<Paciente>(p => p.IdUsuario) // FK para UsuarioBase
                .OnDelete(DeleteBehavior.Cascade); // Se deletar o usuário, deleta o paciente
        }
    }
}
