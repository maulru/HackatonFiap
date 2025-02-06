using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    HorarioInicio = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    HorarioFim = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    Disponibilidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horario");
        }
    }
}
