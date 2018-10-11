using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventModelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedSecurityHoursPerGuard",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SecurityGuards",
                table: "Events",
                newName: "ParkingLotSecurityHours");

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityEndTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityStartTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityEndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SecurityStartTime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ParkingLotSecurityHours",
                table: "Events",
                newName: "SecurityGuards");

            migrationBuilder.AddColumn<double>(
                name: "RequestedSecurityHoursPerGuard",
                table: "Events",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
