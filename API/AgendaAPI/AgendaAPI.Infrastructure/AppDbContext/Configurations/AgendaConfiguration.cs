using AgendaAPI.Domain.Entities.Agenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaAPI.Infrastructure.AppDbContext.Configurations
{
    public class AgendaConfiguration : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.ToTable(nameof(Horario));
            builder.HasKey(x => x.Id);
            builder.Property(p =>  p.Id).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.IdMedico).HasColumnType("int");
            builder.Property(p => p.DataConsulta).HasColumnType("smalldatetime");
            builder.Property(p => p.HorarioInicio).HasColumnType("time(0)");
            builder.Property(p => p.HorarioFim).HasColumnType("time(0)");
            builder.Property(p => p.Disponibilidade).HasColumnType("int");
            builder.Property(p => p.ValorConsulta).HasColumnType("float");

        }
    }
}
