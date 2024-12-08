using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthECAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionPerformance_Missions_MissionId",
                table: "MissionPerformance");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPlan_Missions_MissionId",
                table: "ProjectPlan");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPlan_MissionId",
                table: "ProjectPlan");

            migrationBuilder.DropIndex(
                name: "IX_MissionPerformance_MissionId",
                table: "MissionPerformance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectPlan_MissionId",
                table: "ProjectPlan",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionPerformance_MissionId",
                table: "MissionPerformance",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionPerformance_Missions_MissionId",
                table: "MissionPerformance",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPlan_Missions_MissionId",
                table: "ProjectPlan",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
