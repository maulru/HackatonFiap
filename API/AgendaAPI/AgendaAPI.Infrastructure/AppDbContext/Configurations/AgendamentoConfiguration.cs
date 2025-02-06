using AgendaAPI.Domain.Entities.Agenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaAPI.Infrastructure.AppDbContext.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable(nameof(Agendamento));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.IdPaciente).HasColumnType("int");
            builder.Property(p => p.IdHorario).HasColumnType("int");
            builder.Property(p => p.DataAgendamento).HasColumnType("smalldatetime");
            builder.Property(p => p.Situacao).HasColumnType("int");
            builder.Property(p => p.Observacoes).HasColumnType("nvarchar(500)");

            // Relacionamento com a tabela Horario
            builder.HasOne(a => a.Horario)
                   .WithMany()
                   .HasForeignKey(a => a.IdHorario)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
