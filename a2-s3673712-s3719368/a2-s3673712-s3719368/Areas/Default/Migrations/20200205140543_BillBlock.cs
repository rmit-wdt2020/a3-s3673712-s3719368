using Microsoft.EntityFrameworkCore.Migrations;

namespace a2_s3673712_s3719368.Migrations
{
    public partial class BillBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Block",
                table: "BillPays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "BillPays");
        }
    }
}
