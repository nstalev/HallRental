using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class HallAndEventEntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Halls",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChairTablePerPersonCost",
                table: "Halls",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HallCapacity",
                table: "Halls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityGuardCostPerHour",
                table: "Halls",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "EventTitle",
                table: "Events",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "SecurityGuards",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WithCHairsAndTable",
                table: "Events",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChairTablePerPersonCost",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "HallCapacity",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "SecurityGuardCostPerHour",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "SecurityGuards",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "WithCHairsAndTable",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Halls",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "EventTitle",
                table: "Events",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
