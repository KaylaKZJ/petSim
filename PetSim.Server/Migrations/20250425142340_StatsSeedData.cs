using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class StatsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsDistribution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatsActionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Happiness = table.Column<int>(type: "INTEGER", nullable: false),
                    Hunger = table.Column<int>(type: "INTEGER", nullable: false),
                    Boredom = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsDistribution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatsAction",
                columns: table => new
                {
                    StatsActionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsAction", x => x.StatsActionId);
                    table.ForeignKey(
                        name: "FK_StatsAction_StatsDistribution_StatsActionId",
                        column: x => x.StatsActionId,
                        principalTable: "StatsDistribution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatsAction");

            migrationBuilder.DropTable(
                name: "StatsDistribution");
        }
    }
}
