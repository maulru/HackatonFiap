using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuarioAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoCampoCRM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRM",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
