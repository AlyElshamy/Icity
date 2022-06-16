using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class SeedingMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bec5b7b-d815-4a05-958a-fd8ecade7533");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2ae6538-cf5b-4b74-b189-e3f5e87c0716");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c47cec7-a0be-4a1f-a07c-ac3eb58d6b24", "d5fc7244-e555-43c6-bc05-653102a271f4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "663294d1-e0ff-43ca-b2d0-747e1be8a564", "7181ddd4-db84-4f8c-a587-2e6c2d7356af", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663294d1-e0ff-43ca-b2d0-747e1be8a564");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c47cec7-a0be-4a1f-a07c-ac3eb58d6b24");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2ae6538-cf5b-4b74-b189-e3f5e87c0716", "c02e1483-e8d2-479c-96f7-f48a21548865", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4bec5b7b-d815-4a05-958a-fd8ecade7533", "3d6f8429-a39e-4be7-8ca1-60c7be3ddb7d", "Customer", "CUSTOMER" });
        }
    }
}
