using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EntitiesUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WithCHairsAndTable",
                table: "Events",
                newName: "UsingTablesAndChairs");

            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Events",
                newName: "IsReservationConfirmed");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsingTablesAndChairs",
                table: "Events",
                newName: "WithCHairsAndTable");

            migrationBuilder.RenameColumn(
                name: "IsReservationConfirmed",
                table: "Events",
                newName: "IsConfirmed");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
