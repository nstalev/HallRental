using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class HallEntityUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHallActive",
                table: "Halls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHallActive",
                table: "Halls");
        }
    }
}
