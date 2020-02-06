using Microsoft.EntityFrameworkCore.Migrations;

namespace a2_s3673712_s3719368.Migrations
{
    public partial class addAccountRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber1",
                table: "BillPays");

            migrationBuilder.DropIndex(
                name: "IX_BillPays_AccountNumber1",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "AccountNumber1",
                table: "BillPays");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_AccountNumber",
                table: "BillPays",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber",
                table: "BillPays",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber",
                table: "BillPays");

            migrationBuilder.DropIndex(
                name: "IX_BillPays_AccountNumber",
                table: "BillPays");

            migrationBuilder.AddColumn<int>(
                name: "AccountNumber1",
                table: "BillPays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_AccountNumber1",
                table: "BillPays",
                column: "AccountNumber1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPays_Accounts_AccountNumber1",
                table: "BillPays",
                column: "AccountNumber1",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
