using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalApp.Migrations
{
    public partial class addcolumnTransationTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcountType");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Transactions",
                newName: "ValueCredit");

            migrationBuilder.AddColumn<decimal>(
                name: "ValuDebit",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValuDebit",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ValueCredit",
                table: "Transactions",
                newName: "Value");

            migrationBuilder.CreateTable(
                name: "AcountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcountType", x => x.Id);
                });
        }
    }
}
