using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class listingmedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingBanner",
                table: "AddListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListingLogo",
                table: "AddListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListingPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingPhotos_AddListings_AddListingId",
                        column: x => x.AddListingId,
                        principalTable: "AddListings",
                        principalColumn: "AddListingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddListingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingVideos_AddListings_AddListingId",
                        column: x => x.AddListingId,
                        principalTable: "AddListings",
                        principalColumn: "AddListingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingPhotos_AddListingId",
                table: "ListingPhotos",
                column: "AddListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingVideos_AddListingId",
                table: "ListingVideos",
                column: "AddListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingPhotos");

            migrationBuilder.DropTable(
                name: "ListingVideos");

            migrationBuilder.DropColumn(
                name: "ListingBanner",
                table: "AddListings");

            migrationBuilder.DropColumn(
                name: "ListingLogo",
                table: "AddListings");
        }
    }
}
