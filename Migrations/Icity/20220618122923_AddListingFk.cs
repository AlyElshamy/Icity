using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class AddListingFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AddListings_AddListingId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AddListings_AddListingId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "AddListingId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddListingId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AddListings_AddListingId",
                table: "Branches",
                column: "AddListingId",
                principalTable: "AddListings",
                principalColumn: "AddListingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AddListings_AddListingId",
                table: "Reviews",
                column: "AddListingId",
                principalTable: "AddListings",
                principalColumn: "AddListingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AddListings_AddListingId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AddListings_AddListingId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "AddListingId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AddListingId",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AddListings_AddListingId",
                table: "Branches",
                column: "AddListingId",
                principalTable: "AddListings",
                principalColumn: "AddListingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AddListings_AddListingId",
                table: "Reviews",
                column: "AddListingId",
                principalTable: "AddListings",
                principalColumn: "AddListingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
