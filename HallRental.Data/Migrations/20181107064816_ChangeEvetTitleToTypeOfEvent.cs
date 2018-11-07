using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class ChangeEvetTitleToTypeOfEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityGuardCostPerHour",
                table: "Events",
                newName: "SecurityCostPerHour");

            migrationBuilder.RenameColumn(
                name: "EventTitle",
                table: "Events",
                newName: "TypeOfEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeOfEvent",
                table: "Events",
                newName: "EventTitle");

            migrationBuilder.RenameColumn(
                name: "SecurityCostPerHour",
                table: "Events",
                newName: "SecurityGuardCostPerHour");
        }
    }
}
