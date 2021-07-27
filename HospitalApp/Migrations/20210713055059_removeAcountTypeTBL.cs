using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalApp.Migrations
{
    public partial class removeAcountTypeTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AcountType",
                table: "Transactions");

            //migrationBuilder.DropIndex(
            //    name: "IX_Transactions_AccountTypeId",
            //    table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountTypeId",
                table: "Transactions",
                column: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AcountType",
                table: "Transactions",
                column: "AccountTypeId",
                principalTable: "AcountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
