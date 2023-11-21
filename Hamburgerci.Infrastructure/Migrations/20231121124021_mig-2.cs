using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hamburgerci.Infrastructure.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adet",
                table: "Menuler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Adet",
                table: "EkstraMalzemeler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adet",
                table: "Menuler");

            migrationBuilder.DropColumn(
                name: "Adet",
                table: "EkstraMalzemeler");
        }
    }
}
