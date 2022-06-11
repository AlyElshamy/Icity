using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class testapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109dcef3-1386-4bb3-9a3b-b50b3a0ae837");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a94dcc5-3466-45d0-ba4d-af26ab63217b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15d5190e-b598-439d-9eb3-12ddf2aadf9f", "3889a59d-0759-49c4-a4f5-88429b2a9f73", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a0645b0-b343-4afb-a955-fdfbf7e8ae6a", "0d0f13c6-a0f4-4a8d-8a5c-012a57d3537e", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15d5190e-b598-439d-9eb3-12ddf2aadf9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a0645b0-b343-4afb-a955-fdfbf7e8ae6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "109dcef3-1386-4bb3-9a3b-b50b3a0ae837", "f44af818-005e-4f70-b6a8-7543b59c03fc", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a94dcc5-3466-45d0-ba4d-af26ab63217b", "185b4850-f757-463c-b333-47d353ccf90f", "Customer", "CUSTOMER" });
        }
    }
}
