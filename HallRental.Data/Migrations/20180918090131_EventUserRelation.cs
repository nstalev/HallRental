using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class EventUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_TenantId",
                table: "Events",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_TenantId",
                table: "Events",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_TenantId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TenantId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Events");
        }
    }
}
