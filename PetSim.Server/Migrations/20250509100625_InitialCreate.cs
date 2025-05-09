using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatsDistribution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatsActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stats_Happiness = table.Column<int>(type: "int", nullable: false),
                    Stats_Hunger = table.Column<int>(type: "int", nullable: false),
                    Stats_Boredom = table.Column<int>(type: "int", nullable: false),
                    Stats_Tiredness = table.Column<int>(type: "int", nullable: false),
                    Stats_Weight = table.Column<int>(type: "int", nullable: false),
                    Stats_Loneliness = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsDistribution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_PetTypes_PetTypeID",
                        column: x => x.PetTypeID,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatsAction",
                columns: table => new
                {
                    StatsActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stats_Happiness = table.Column<int>(type: "int", nullable: false),
                    Stats_Hunger = table.Column<int>(type: "int", nullable: false),
                    Stats_Boredom = table.Column<int>(type: "int", nullable: false),
                    Stats_Tiredness = table.Column<int>(type: "int", nullable: false),
                    Stats_Weight = table.Column<int>(type: "int", nullable: false),
                    Stats_Loneliness = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetTypeID",
                table: "Pet",
                column: "PetTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PetId",
                table: "Stats",
                column: "PetId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "StatsAction");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "StatsDistribution");

            migrationBuilder.DropTable(
                name: "PetTypes");
        }
    }
}
