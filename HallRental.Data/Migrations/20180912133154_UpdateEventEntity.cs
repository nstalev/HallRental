using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class UpdateEventEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HallRentalPrice",
                table: "Events",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityGuardCostPerHour",
                table: "Events",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityPrice",
                table: "Events",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TablesAndChairsPrice",
                table: "Events",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HallRentalPrice",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SecurityGuardCostPerHour",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SecurityPrice",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TablesAndChairsPrice",
                table: "Events");
        }
    }
}
