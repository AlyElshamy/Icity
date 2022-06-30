using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class ClassifiedAdsModelsUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassifiedAds_ClassifiedAdsTypes_ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds");

            migrationBuilder.DropIndex(
                name: "IX_ClassifiedAds_ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds");

            migrationBuilder.DropColumn(
                name: "ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ClassifiedAdsTypes",
                newName: "ClassifiedAdsTypeID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "ClassifiedAds",
                newName: "ClassifiedAdsTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedAds_ClassifiedAdsTypeID",
                table: "ClassifiedAds",
                column: "ClassifiedAdsTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassifiedAds_ClassifiedAdsTypes_ClassifiedAdsTypeID",
                table: "ClassifiedAds",
                column: "ClassifiedAdsTypeID",
                principalTable: "ClassifiedAdsTypes",
                principalColumn: "ClassifiedAdsTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassifiedAds_ClassifiedAdsTypes_ClassifiedAdsTypeID",
                table: "ClassifiedAds");

            migrationBuilder.DropIndex(
                name: "IX_ClassifiedAds_ClassifiedAdsTypeID",
                table: "ClassifiedAds");

            migrationBuilder.RenameColumn(
                name: "ClassifiedAdsTypeID",
                table: "ClassifiedAdsTypes",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "ClassifiedAdsTypeID",
                table: "ClassifiedAds",
                newName: "TypeId");

            migrationBuilder.AddColumn<int>(
                name: "ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassifiedAds_ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds",
                column: "ClassifiedAdsTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassifiedAds_ClassifiedAdsTypes_ClassifiedAdsTypeTypeId",
                table: "ClassifiedAds",
                column: "ClassifiedAdsTypeTypeId",
                principalTable: "ClassifiedAdsTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
