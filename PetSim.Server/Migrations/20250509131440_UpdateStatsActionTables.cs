using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatsActionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_PetTypes_PetTypeID",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_PetStats_Pet_PetId",
                table: "PetStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_PetTypeID",
                table: "Pet");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "Pets");

            migrationBuilder.AddColumn<Guid>(
                name: "StatsDistributionId",
                table: "StatsAction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetStats_Pets_PetId",
                table: "PetStats",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetStats_Pets_PetId",
                table: "PetStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "StatsDistributionId",
                table: "StatsAction");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pets");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "Pet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetTypeID",
                table: "Pet",
                column: "PetTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_PetTypes_PetTypeID",
                table: "Pet",
                column: "PetTypeID",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetStats_Pet_PetId",
                table: "PetStats",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
