using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionServices_BankAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionUniqeId = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    SourceAccount = table.Column<string>(type: "TEXT", nullable: false),
                    DestinationAccount = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
