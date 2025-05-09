using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetSim.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPetAndStatsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Pet_PetId",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stats",
                table: "Stats");

            migrationBuilder.RenameTable(
                name: "Stats",
                newName: "PetStats");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_PetId",
                table: "PetStats",
                newName: "IX_PetStats_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetStats",
                table: "PetStats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetStats_Pet_PetId",
                table: "PetStats",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetStats_Pet_PetId",
                table: "PetStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetStats",
                table: "PetStats");

            migrationBuilder.RenameTable(
                name: "PetStats",
                newName: "Stats");

            migrationBuilder.RenameIndex(
                name: "IX_PetStats_PetId",
                table: "Stats",
                newName: "IX_Stats_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stats",
                table: "Stats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Pet_PetId",
                table: "Stats",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
