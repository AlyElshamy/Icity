using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class BranchMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Branches");
        }
    }
}
