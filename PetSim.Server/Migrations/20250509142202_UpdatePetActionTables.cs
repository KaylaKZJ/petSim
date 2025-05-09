using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePetActionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PetStatsId",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetStatsId",
                table: "Pets");
        }
    }
}
