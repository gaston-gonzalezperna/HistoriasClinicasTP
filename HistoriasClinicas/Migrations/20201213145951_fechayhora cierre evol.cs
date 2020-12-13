using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HistoriasClinicas2.Migrations
{
    public partial class fechayhoracierreevol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraAlta",
                table: "Evoluciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraCierre",
                table: "Evoluciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaYHoraAlta",
                table: "Evoluciones");

            migrationBuilder.DropColumn(
                name: "FechaYHoraCierre",
                table: "Evoluciones");
        }
    }
}
