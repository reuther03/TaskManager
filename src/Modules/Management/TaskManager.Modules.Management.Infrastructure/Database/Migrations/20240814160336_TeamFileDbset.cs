using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class TeamFileDbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamFile_Teams_TeamId",
                schema: "management",
                table: "TeamFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamFile",
                schema: "management",
                table: "TeamFile");

            migrationBuilder.RenameTable(
                name: "TeamFile",
                schema: "management",
                newName: "TeamFiles",
                newSchema: "management");

            migrationBuilder.RenameIndex(
                name: "IX_TeamFile_TeamId",
                schema: "management",
                table: "TeamFiles",
                newName: "IX_TeamFiles_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamFiles",
                schema: "management",
                table: "TeamFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamFiles_Teams_TeamId",
                schema: "management",
                table: "TeamFiles",
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
                name: "FK_TeamFiles_Teams_TeamId",
                schema: "management",
                table: "TeamFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamFiles",
                schema: "management",
                table: "TeamFiles");

            migrationBuilder.RenameTable(
                name: "TeamFiles",
                schema: "management",
                newName: "TeamFile",
                newSchema: "management");

            migrationBuilder.RenameIndex(
                name: "IX_TeamFiles_TeamId",
                schema: "management",
                table: "TeamFile",
                newName: "IX_TeamFile_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamFile",
                schema: "management",
                table: "TeamFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamFile_Teams_TeamId",
                schema: "management",
                table: "TeamFile",
                column: "TeamId",
                principalSchema: "management",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
