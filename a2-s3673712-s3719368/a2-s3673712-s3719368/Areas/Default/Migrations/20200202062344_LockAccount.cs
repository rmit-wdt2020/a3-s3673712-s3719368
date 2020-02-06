using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace a2_s3673712_s3719368.Migrations
{
    public partial class LockAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lock",
                table: "Logins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockDate",
                table: "Logins",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "attempt",
                table: "Logins",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lock",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "LockDate",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "attempt",
                table: "Logins");
        }
    }
}
