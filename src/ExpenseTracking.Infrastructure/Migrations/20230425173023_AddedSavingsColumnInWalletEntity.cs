using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracking.Infrastructure.Migrations
{
    public partial class AddedSavingsColumnInWalletEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Savings",
                table: "Wallets",
                type: "decimal(14,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Savings",
                table: "Wallets");
        }
    }
}
