using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventEntityUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityServiceHoursPerGuard",
                table: "Events",
                newName: "RequestedSecurityHoursPerGuard");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Events",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventEnd",
                table: "Events",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventStart",
                table: "Events",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEnd",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventStart",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "RequestedSecurityHoursPerGuard",
                table: "Events",
                newName: "SecurityServiceHoursPerGuard");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Events",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
