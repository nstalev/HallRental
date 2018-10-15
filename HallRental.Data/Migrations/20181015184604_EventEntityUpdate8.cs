using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventEntityUpdate8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityPrice",
                table: "Events",
                newName: "SecurityDeposit");

            migrationBuilder.AddColumn<decimal>(
                name: "ParkingLotSecurityPrice",
                table: "Events",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingLotSecurityPrice",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SecurityDeposit",
                table: "Events",
                newName: "SecurityPrice");
        }
    }
}
