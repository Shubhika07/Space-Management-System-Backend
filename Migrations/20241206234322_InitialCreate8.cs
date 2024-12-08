using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthECAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScientificData_Missions_MissionId",
                table: "ScientificData");

            migrationBuilder.DropIndex(
                name: "IX_ScientificData_MissionId",
                table: "ScientificData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScientificData_MissionId",
                table: "ScientificData",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificData_Missions_MissionId",
                table: "ScientificData",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
