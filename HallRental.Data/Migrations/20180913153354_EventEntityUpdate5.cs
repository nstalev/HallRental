using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventEntityUpdate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChairTablePerPersonCost",
                table: "Halls",
                newName: "TablesAndChairsCostPerPerson");

            migrationBuilder.AddColumn<string>(
                name: "Caterer",
                table: "Events",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TablesAndChairsCostPerPerson",
                table: "Events",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caterer",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TablesAndChairsCostPerPerson",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TablesAndChairsCostPerPerson",
                table: "Halls",
                newName: "ChairTablePerPersonCost");
        }
    }
}
