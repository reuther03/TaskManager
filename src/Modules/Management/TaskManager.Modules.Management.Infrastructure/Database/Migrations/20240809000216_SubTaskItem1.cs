using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Modules.Management.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SubTaskItem1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTaskItem_TaskItems_TaskItemId",
                schema: "management",
                table: "SubTaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTaskItem",
                schema: "management",
                table: "SubTaskItem");

            migrationBuilder.RenameTable(
                name: "SubTaskItem",
                schema: "management",
                newName: "SubTaskItems",
                newSchema: "management");

            migrationBuilder.RenameIndex(
                name: "IX_SubTaskItem_TaskItemId",
                schema: "management",
                table: "SubTaskItems",
                newName: "IX_SubTaskItems_TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTaskItems",
                schema: "management",
                table: "SubTaskItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaskItems_TaskItems_TaskItemId",
                schema: "management",
                table: "SubTaskItems",
                column: "TaskItemId",
                principalSchema: "management",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTaskItems_TaskItems_TaskItemId",
                schema: "management",
                table: "SubTaskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTaskItems",
                schema: "management",
                table: "SubTaskItems");

            migrationBuilder.RenameTable(
                name: "SubTaskItems",
                schema: "management",
                newName: "SubTaskItem",
                newSchema: "management");

            migrationBuilder.RenameIndex(
                name: "IX_SubTaskItems_TaskItemId",
                schema: "management",
                table: "SubTaskItem",
                newName: "IX_SubTaskItem_TaskItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTaskItem",
                schema: "management",
                table: "SubTaskItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaskItem_TaskItems_TaskItemId",
                schema: "management",
                table: "SubTaskItem",
                column: "TaskItemId",
                principalSchema: "management",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
