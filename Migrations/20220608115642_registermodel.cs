using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class registermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "057ffbc2-0975-4956-9b52-cf3cf5e1ece0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3df68ab-43ba-4edd-8eae-6a1d30b8848c");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1856b1a0-9d9d-4b22-9c2e-bc19f1cec37c", "18d759eb-ab47-45d5-bdaa-27183f02f020", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0efa8e4-1234-42a7-83eb-ce9625cf38ee", "c1141aa0-6a0e-409d-9381-96eb24158f8a", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1856b1a0-9d9d-4b22-9c2e-bc19f1cec37c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0efa8e4-1234-42a7-83eb-ce9625cf38ee");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3df68ab-43ba-4edd-8eae-6a1d30b8848c", "63c6aa2b-25b4-48c3-9cb8-e1b704598b44", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "057ffbc2-0975-4956-9b52-cf3cf5e1ece0", "69453a06-9ef8-46fd-b8fb-e412c42caa5c", "Customer", "CUSTOMER" });
        }
    }
}
