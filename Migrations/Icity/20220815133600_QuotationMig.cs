using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class QuotationMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "AddListings");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "AddListings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Quotations",
                columns: table => new
                {
                    QuotationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassifiedAdsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotations", x => x.QuotationId);
                    table.ForeignKey(
                        name: "FK_Quotations_ClassifiedAds_ClassifiedAdsID",
                        column: x => x.ClassifiedAdsID,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddListings_CountryId",
                table: "AddListings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_ClassifiedAdsID",
                table: "Quotations",
                column: "ClassifiedAdsID");

            migrationBuilder.AddForeignKey(
                name: "FK_AddListings_Countries_CountryId",
                table: "AddListings",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddListings_Countries_CountryId",
                table: "AddListings");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_AddListings_CountryId",
                table: "AddListings");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AddListings");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AddListings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
