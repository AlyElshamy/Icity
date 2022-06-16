using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class ProfileModelUpIden5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_ApplicationId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_ApplicationId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeEvents_AspNetUsers_ApplicationId",
                table: "LifeEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_ApplicationId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_ApplicationId",
                table: "Videos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11897ffe-501a-4f45-a15d-3ca93a6681a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f17d254-83d3-4387-a511-0a5ab46ac2ab");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "LifeEvents");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Videos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_ApplicationId",
                table: "Videos",
                newName: "IX_Videos_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Skills",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_ApplicationId",
                table: "Skills",
                newName: "IX_Skills_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "LifeEvents",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_LifeEvents_ApplicationId",
                table: "LifeEvents",
                newName: "IX_LifeEvents_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Languages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_ApplicationId",
                table: "Languages",
                newName: "IX_Languages_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Interests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_ApplicationId",
                table: "Interests",
                newName: "IX_Interests_Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Educations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_ApplicationId",
                table: "Educations",
                newName: "IX_Educations_Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bcbe7a88-1312-48bd-bf90-bbe65223334d", "05dd93ae-3eef-4771-8324-115e2e111995", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d63fd47-5c39-4975-8ce0-48ae6befc8cd", "00513b81-c403-49a7-a11e-1abf507a445d", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_Id",
                table: "Educations",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_AspNetUsers_Id",
                table: "Interests",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_Id",
                table: "Languages",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeEvents_AspNetUsers_Id",
                table: "LifeEvents",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_Id",
                table: "Skills",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_Id",
                table: "Videos",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_Id",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_AspNetUsers_Id",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_Id",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeEvents_AspNetUsers_Id",
                table: "LifeEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_Id",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_Id",
                table: "Videos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d63fd47-5c39-4975-8ce0-48ae6befc8cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcbe7a88-1312-48bd-bf90-bbe65223334d");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Videos",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_Id",
                table: "Videos",
                newName: "IX_Videos_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Skills",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_Id",
                table: "Skills",
                newName: "IX_Skills_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LifeEvents",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_LifeEvents_Id",
                table: "LifeEvents",
                newName: "IX_LifeEvents_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Languages",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_Id",
                table: "Languages",
                newName: "IX_Languages_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Interests",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_Id",
                table: "Interests",
                newName: "IX_Interests_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Educations",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_Id",
                table: "Educations",
                newName: "IX_Educations_ApplicationId");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "LifeEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Interests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f17d254-83d3-4387-a511-0a5ab46ac2ab", "8c825435-c401-41d2-802a-e486cae69979", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11897ffe-501a-4f45-a15d-3ca93a6681a0", "f144e9b1-fd28-4c95-9a13-d1d5b4be2765", "Customer", "CUSTOMER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_ApplicationId",
                table: "Educations",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationId",
                table: "Interests",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_ApplicationId",
                table: "Languages",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeEvents_AspNetUsers_ApplicationId",
                table: "LifeEvents",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_ApplicationId",
                table: "Skills",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_ApplicationId",
                table: "Videos",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
