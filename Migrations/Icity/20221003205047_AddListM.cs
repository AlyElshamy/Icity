using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class AddListM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AddListings");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AddListings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AddListings");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AddListings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
