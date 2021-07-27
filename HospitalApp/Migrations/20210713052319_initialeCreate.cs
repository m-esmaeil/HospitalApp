using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalApp.Migrations
{
    public partial class initialeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AcountType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AcountType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Categories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categories", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EntriesSerialize",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Serial = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EntriesSerialize", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AccountsTree",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
            //        FollowTo = table.Column<int>(type: "int", nullable: true),
            //        CategoryId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AccountsTree", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AccountsTree_Categories",
            //            column: x => x.CategoryId,
            //            principalTable: "Categories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Transactions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "datetime", nullable: false),
            //        SerialNumberId = table.Column<int>(type: "int", nullable: false),
            //        AccountTreeId = table.Column<int>(type: "int", nullable: false),
            //        Value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
            //        AccountTypeId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Transactions", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Transactions_AccountsTree",
            //            column: x => x.AccountTreeId,
            //            principalTable: "AccountsTree",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Transactions_AcountType",
            //            column: x => x.AccountTypeId,
            //            principalTable: "AcountType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Transactions_EntriesSerialize",
            //            column: x => x.SerialNumberId,
            //            principalTable: "EntriesSerialize",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AccountsTree_CategoryId",
            //    table: "AccountsTree",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Transactions_AccountTreeId",
            //    table: "Transactions",
            //    column: "AccountTreeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Transactions_AccountTypeId",
            //    table: "Transactions",
            //    column: "AccountTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Transactions_SerialNumberId",
            //    table: "Transactions",
            //    column: "SerialNumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Transactions");

            //migrationBuilder.DropTable(
            //    name: "AccountsTree");

            //migrationBuilder.DropTable(
            //    name: "AcountType");

            //migrationBuilder.DropTable(
            //    name: "EntriesSerialize");

            //migrationBuilder.DropTable(
            //    name: "Categories");
        }
    }
}
