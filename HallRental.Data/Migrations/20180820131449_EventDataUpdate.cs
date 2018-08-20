using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEdn",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventStart",
                table: "Events",
                newName: "EventDate");

            migrationBuilder.AddColumn<int>(
                name: "RentTime",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentTime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Events",
                newName: "EventStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEdn",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
