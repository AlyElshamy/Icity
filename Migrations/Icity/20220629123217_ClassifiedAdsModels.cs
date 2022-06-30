using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class ClassifiedAdsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.CreateTable(
                name: "ClassifiedAdsTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedAdsTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatuses",
                columns: table => new
                {
                    ProductStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatuses", x => x.ProductStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedAds",
                columns: table => new
                {
                    ClassifiedAdsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassifiedAdsLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassifiedAdsTypeTypeId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ProductStatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedAds", x => x.ClassifiedAdsID);
                    table.ForeignKey(
                        name: "FK_ClassifiedAds_ClassifiedAdsTypes_ClassifiedAdsTypeTypeId",
                        column: x => x.ClassifiedAdsTypeTypeId,
                        principalTable: "ClassifiedAdsTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassifiedAds_ProductStatuses_ProductStatusID",
                        column: x => x.ProductStatusID,
                        principalTable: "ProductStatuses",
                        principalColumn: "ProductStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassifiedAsdMedias",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassifiedAdsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedAsdMedias", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_ClassifiedAsdMedias_ClassifiedAds_ClassifiedAdsID",
                        column: x => x.ClassifiedAdsID,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedAds_ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds",
                column: "ClassifiedAdsTypeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedAds_ProductStatusID",
                table: "ClassifiedAds",
                column: "ProductStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedAsdMedias_ClassifiedAdsID",
                table: "ClassifiedAsdMedias",
                column: "ClassifiedAdsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassifiedAsdMedias");

            migrationBuilder.DropTable(
                name: "ClassifiedAds");

            migrationBuilder.DropTable(
                name: "ClassifiedAdsTypes");

            migrationBuilder.DropTable(
                name: "ProductStatuses");

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }
    }
}
