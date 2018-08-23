using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventHallRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_HallId",
                table: "Events",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_HallId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Events");
        }
    }
}
