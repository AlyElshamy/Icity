using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class newModelsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityTypes",
                columns: table => new
                {
                    EntityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypes", x => x.EntityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteClassifieds",
                columns: table => new
                {
                    FavouriteClassifiedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassifiedAdsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteClassifieds", x => x.FavouriteClassifiedId);
                    table.ForeignKey(
                        name: "FK_FavouriteClassifieds_ClassifiedAds_ClassifiedAdsID",
                        column: x => x.ClassifiedAdsID,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteProfiles",
                columns: table => new
                {
                    FavouriteProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteProfiles", x => x.FavouriteProfileId);
                });

            migrationBuilder.CreateTable(
                name: "FolowClassifieds",
                columns: table => new
                {
                    FolowClassifiedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassifiedAdsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolowClassifieds", x => x.FolowClassifiedId);
                    table.ForeignKey(
                        name: "FK_FolowClassifieds_ClassifiedAds_ClassifiedAdsID",
                        column: x => x.ClassifiedAdsID,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolowProfile",
                columns: table => new
                {
                    FolowProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolowProfile", x => x.FolowProfileId);
                });

            migrationBuilder.InsertData(
                table: "EntityTypes",
                columns: new[] { "EntityTypeId", "EntityTitle" },
                values: new object[] { 1, "Profile" });

            migrationBuilder.InsertData(
                table: "EntityTypes",
                columns: new[] { "EntityTypeId", "EntityTitle" },
                values: new object[] { 2, "Bussiness" });

            migrationBuilder.InsertData(
                table: "EntityTypes",
                columns: new[] { "EntityTypeId", "EntityTitle" },
                values: new object[] { 3, "Classified" });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteClassifieds_ClassifiedAdsID",
                table: "FavouriteClassifieds",
                column: "ClassifiedAdsID");

            migrationBuilder.CreateIndex(
                name: "IX_FolowClassifieds_ClassifiedAdsID",
                table: "FolowClassifieds",
                column: "ClassifiedAdsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityTypes");

            migrationBuilder.DropTable(
                name: "FavouriteClassifieds");

            migrationBuilder.DropTable(
                name: "FavouriteProfiles");

            migrationBuilder.DropTable(
                name: "FolowClassifieds");

            migrationBuilder.DropTable(
                name: "FolowProfile");
        }
    }
}
