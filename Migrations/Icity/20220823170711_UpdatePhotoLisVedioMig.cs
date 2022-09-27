using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations.Icity
{
    public partial class UpdatePhotoLisVedioMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "ListingVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "ListingVideos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "ListingPhotos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "ListingPhotos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "ListingVideos");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "ListingVideos");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "ListingPhotos");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "ListingPhotos");
        }
    }
}
