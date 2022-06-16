using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class ProfileModelUpIden4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationId",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a75b819-ad86-48c1-9dca-bed01d2c8e29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be746f1-bec1-4a0d-a327-7c067e1e7ab2");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Photos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ApplicationId",
                table: "Photos",
                newName: "IX_Photos_Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f17d254-83d3-4387-a511-0a5ab46ac2ab", "8c825435-c401-41d2-802a-e486cae69979", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11897ffe-501a-4f45-a15d-3ca93a6681a0", "f144e9b1-fd28-4c95-9a13-d1d5b4be2765", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_Id",
                table: "Photos",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_Id",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11897ffe-501a-4f45-a15d-3ca93a6681a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f17d254-83d3-4387-a511-0a5ab46ac2ab");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Photos",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_Id",
                table: "Photos",
                newName: "IX_Photos_ApplicationId");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8be746f1-bec1-4a0d-a327-7c067e1e7ab2", "8d225d5e-569b-4065-9b11-bb36173bc807", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a75b819-ad86-48c1-9dca-bed01d2c8e29", "8bcbaacd-54db-4f5d-b7ea-07415fc8690e", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationId",
                table: "Photos",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
