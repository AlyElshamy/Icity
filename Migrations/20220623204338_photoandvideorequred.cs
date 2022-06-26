using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class photoandvideorequred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "66c06a57-9b3e-4926-88c6-873807a13e1a", "87a7b7d9-fa06-4188-bd25-e988f551be05", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6021352a-a844-4669-bd45-e4763225ceb4", "5e0777f1-d51e-46ac-ba0d-d409b5c1484f", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6021352a-a844-4669-bd45-e4763225ceb4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66c06a57-9b3e-4926-88c6-873807a13e1a");

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
    }
}
