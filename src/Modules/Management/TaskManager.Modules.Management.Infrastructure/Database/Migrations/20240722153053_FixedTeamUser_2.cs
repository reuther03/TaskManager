using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixedTeamUser_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUserIds_Teams_UserIds1",
                schema: "management",
                table: "TeamUserIds");

            migrationBuilder.RenameColumn(
                name: "UserIds1",
                schema: "management",
                table: "TeamUserIds",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUserIds_UserIds1",
                schema: "management",
                table: "TeamUserIds",
                newName: "IX_TeamUserIds_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUserIds_Teams_TeamId",
                schema: "management",
                table: "TeamUserIds",
                column: "TeamId",
                principalSchema: "management",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUserIds_Teams_TeamId",
                schema: "management",
                table: "TeamUserIds");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                schema: "management",
                table: "TeamUserIds",
                newName: "UserIds1");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUserIds_TeamId",
                schema: "management",
                table: "TeamUserIds",
                newName: "IX_TeamUserIds_UserIds1");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUserIds_Teams_UserIds1",
                schema: "management",
                table: "TeamUserIds",
                column: "UserIds1",
                principalSchema: "management",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
