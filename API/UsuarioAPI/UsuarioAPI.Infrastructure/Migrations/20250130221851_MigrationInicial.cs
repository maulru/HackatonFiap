using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuarioAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCRM = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Paciente");
        }
    }
}
