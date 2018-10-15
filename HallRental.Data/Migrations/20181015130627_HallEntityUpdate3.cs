using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class HallEntityUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDepositAfter10pm",
                table: "Halls",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDepositBefore10pm",
                table: "Halls",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityDepositAfter10pm",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "SecurityDepositBefore10pm",
                table: "Halls");
        }
    }
}
