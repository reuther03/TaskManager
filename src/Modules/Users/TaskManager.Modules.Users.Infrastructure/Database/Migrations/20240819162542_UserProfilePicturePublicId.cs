﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Modules.Users.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserProfilePicturePublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePublicId",
                schema: "users",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePublicId",
                schema: "users",
                table: "Users");
        }
    }
}
