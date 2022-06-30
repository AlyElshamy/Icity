using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class SeedingClassified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClassifiedAdsTypes",
                columns: new[] { "ClassifiedAdsTypeID", "SortOrder", "TypePic", "TypeTitleAr", "TypeTitleEn" },
                values: new object[,]
                {
                    { 1, null, null, null, "Vehicles" },
                    { 2, null, null, null, "Property" },
                    { 3, null, null, null, "Electronics" },
                    { 4, null, null, null, "Home&&Office Furniture – Decorations" },
                    { 5, null, null, null, "Fashion" },
                    { 6, null, null, null, "Pets" },
                    { 7, null, null, null, "Kids" },
                    { 8, null, null, null, "Books" },
                    { 9, null, null, null, "Sports" },
                    { 10, null, null, null, "Supermarket" },
                    { 11, null, null, null, "Health&&Beauty" },
                    { 12, null, null, null, "Gamming" },
                    { 13, null, null, null, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "ProductStatuses",
                columns: new[] { "ProductStatusID", "StatusTitle" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Used" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ClassifiedAdsTypes",
                keyColumn: "ClassifiedAdsTypeID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductStatuses",
                keyColumn: "ProductStatusID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductStatuses",
                keyColumn: "ProductStatusID",
                keyValue: 2);
        }
    }
}
