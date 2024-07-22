using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixedTeamUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "management",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "management",
                newName: "TeamUsers",
                newSchema: "management");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamUsers",
                schema: "management",
                table: "TeamUsers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamUsers",
                schema: "management",
                table: "TeamUsers");

            migrationBuilder.RenameTable(
                name: "TeamUsers",
                schema: "management",
                newName: "Users",
                newSchema: "management");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "management",
                table: "Users",
                column: "Id");
        }
    }
}
