using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class Favourite_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    FavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.FavouriteId);
                    table.ForeignKey(
                        name: "FK_Favourites_AddListings_AddListingId",
                        column: x => x.AddListingId,
                        principalTable: "AddListings",
                        principalColumn: "AddListingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AddListingId",
                table: "Favourites",
                column: "AddListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");
        }
    }
}
