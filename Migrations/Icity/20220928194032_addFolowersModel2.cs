using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class addFolowersModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BussinessId",
                table: "Folwers");

            migrationBuilder.AddColumn<int>(
                name: "AddListingId",
                table: "Folwers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Folwers_AddListingId",
                table: "Folwers",
                column: "AddListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folwers_AddListings_AddListingId",
                table: "Folwers",
                column: "AddListingId",
                principalTable: "AddListings",
                principalColumn: "AddListingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folwers_AddListings_AddListingId",
                table: "Folwers");

            migrationBuilder.DropIndex(
                name: "IX_Folwers_AddListingId",
                table: "Folwers");

            migrationBuilder.DropColumn(
                name: "AddListingId",
                table: "Folwers");

            migrationBuilder.AddColumn<string>(
                name: "BussinessId",
                table: "Folwers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
