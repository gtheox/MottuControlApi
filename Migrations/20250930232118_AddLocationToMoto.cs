using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuControlApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToMoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Motos",
                type: "BINARY_DOUBLE",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Motos",
                type: "BINARY_DOUBLE",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaAtualizacaoLocalizacao",
                table: "Motos",
                type: "TIMESTAMP(7)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Motos");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Motos");

            migrationBuilder.DropColumn(
                name: "UltimaAtualizacaoLocalizacao",
                table: "Motos");
        }
    }
}
