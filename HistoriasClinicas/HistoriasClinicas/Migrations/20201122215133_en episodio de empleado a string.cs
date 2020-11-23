using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoriasClinicas.Migrations
{
    public partial class enepisodiodeempleadoastring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodios_Usuarios_EmpleadoRegistraId",
                table: "Episodios");

            migrationBuilder.DropIndex(
                name: "IX_Episodios_EmpleadoRegistraId",
                table: "Episodios");

            migrationBuilder.DropColumn(
                name: "EmpleadoRegistraId",
                table: "Episodios");

            migrationBuilder.AddColumn<string>(
                name: "EmpleadoRegistra",
                table: "Episodios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpleadoRegistra",
                table: "Episodios");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoRegistraId",
                table: "Episodios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_EmpleadoRegistraId",
                table: "Episodios",
                column: "EmpleadoRegistraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodios_Usuarios_EmpleadoRegistraId",
                table: "Episodios",
                column: "EmpleadoRegistraId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
