using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracking.Infrastructure.Migrations
{
    public partial class AddedIncomesAndExpensesPerDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseForDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expense = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    DayOfMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseForDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseForDays_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeForDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    DayOfMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeForDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeForDays_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseForDays_WalletId",
                table: "ExpenseForDays",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeForDays_WalletId",
                table: "IncomeForDays",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseForDays");

            migrationBuilder.DropTable(
                name: "IncomeForDays");
        }
    }
}
