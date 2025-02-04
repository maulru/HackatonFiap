using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Entities.Base;

namespace UsuarioAPI.Infrastructure.AppDbContext.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioBase>
    {
        public void Configure(EntityTypeBuilder<UsuarioBase> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnType("NVARCHAR(450)").IsRequired();
            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.CPF).HasColumnName("CPF").HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Senha).HasColumnName("Senha").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Tipo).HasColumnName("Tipo").HasColumnType("VARCHAR(3)").IsRequired();
        }
    }
}
