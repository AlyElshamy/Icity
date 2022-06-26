using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class photoandvideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663294d1-e0ff-43ca-b2d0-747e1be8a564");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c47cec7-a0be-4a1f-a07c-ac3eb58d6b24");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c079852-1f04-4535-8c33-ceb40f6f3ee2", "d8cbe7d8-974a-41e9-81bc-6a84088359f4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1a0fe88-94f4-44ad-8aa0-00bb6f623179", "6df26b8e-b0d8-4a74-9d18-bfabfddec58d", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c079852-1f04-4535-8c33-ceb40f6f3ee2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1a0fe88-94f4-44ad-8aa0-00bb6f623179");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c47cec7-a0be-4a1f-a07c-ac3eb58d6b24", "d5fc7244-e555-43c6-bc05-653102a271f4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "663294d1-e0ff-43ca-b2d0-747e1be8a564", "7181ddd4-db84-4f8c-a587-2e6c2d7356af", "Customer", "CUSTOMER" });
        }
    }
}
