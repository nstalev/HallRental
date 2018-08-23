using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HallRental.Data.Migrations
{
    public partial class HallEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    MondayFriday8amTo3pm = table.Column<decimal>(nullable: false),
                    MondayThursday4pmToMN = table.Column<decimal>(nullable: false),
                    Friday4pmToMN = table.Column<decimal>(nullable: false),
                    Saturday8amTo3pm = table.Column<decimal>(nullable: false),
                    Saturday4pmToMN = table.Column<decimal>(nullable: false),
                    Sunday8amTo3pm = table.Column<decimal>(nullable: false),
                    Sunday4pmToMN = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Halls");
        }
    }
}
