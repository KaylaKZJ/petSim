using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPetTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatsAction_StatsDistribution_StatsActionId",
                table: "StatsAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatsAction",
                table: "StatsAction");

            migrationBuilder.DropColumn(
                name: "StatsActionId",
                table: "StatsAction");

            migrationBuilder.RenameColumn(
                name: "PetTypeID",
                table: "StatsAction",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "StatsActionId",
                table: "PetTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatsAction",
                table: "StatsAction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PetTypes_StatsActionId",
                table: "PetTypes",
                column: "StatsActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetTypes_StatsAction_StatsActionId",
                table: "PetTypes",
                column: "StatsActionId",
                principalTable: "StatsAction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatsAction_StatsDistribution_Id",
                table: "StatsAction",
                column: "Id",
                principalTable: "StatsDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetTypes_StatsAction_StatsActionId",
                table: "PetTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_StatsAction_StatsDistribution_Id",
                table: "StatsAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatsAction",
                table: "StatsAction");

            migrationBuilder.DropIndex(
                name: "IX_PetTypes_StatsActionId",
                table: "PetTypes");

            migrationBuilder.DropColumn(
                name: "StatsActionId",
                table: "PetTypes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StatsAction",
                newName: "PetTypeID");

            migrationBuilder.AddColumn<Guid>(
                name: "StatsActionId",
                table: "StatsAction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatsAction",
                table: "StatsAction",
                column: "StatsActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatsAction_StatsDistribution_StatsActionId",
                table: "StatsAction",
                column: "StatsActionId",
                principalTable: "StatsDistribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
