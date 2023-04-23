using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracking.Infrastructure.Migrations
{
    public partial class AddedCurrencyPropertyToWalletEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Wallets");
        }
    }
}
