using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class CityModelM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Countries",
                newName: "CountryTlEn");

            migrationBuilder.AddColumn<string>(
                name: "CountryTlAr",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryTlAr",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "CountryTlEn",
                table: "Countries",
                newName: "Title");
        }
    }
}
