using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalApp.Migrations
{
    public partial class modifyDecimalInTRansaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Date",
            //    table: "Transactions");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValueCredit",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValuDebit",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "Date",
            //    table: "EntriesSerialize",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValueSql: "GETDATE()",
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValueCredit",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValuDebit",
                table: "Transactions",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Date",
            //    table: "Transactions",
            //    type: "datetime",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "Date",
            //    table: "EntriesSerialize",
            //    type: "datetime2",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime",
            //    oldNullable: false,
            //    oldDefaultValueSql: "GETDATE()");
        }
    }
}
