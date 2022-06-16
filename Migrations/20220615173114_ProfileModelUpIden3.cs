using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class ProfileModelUpIden3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_AspNetUsers_UserId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_AspNetUsers_UserId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeEvents_AspNetUsers_UserId",
                table: "LifeEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationUserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_UserId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_UserId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Skills_UserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_LifeEvents_UserId",
                table: "LifeEvents");

            migrationBuilder.DropIndex(
                name: "IX_Languages_UserId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Interests_UserId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UserId",
                table: "Educations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70afc07a-89a0-4860-8a51-f6244b4dde09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71352d4b-6646-4fcc-9d3e-524071b8fa91");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Videos",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Skills",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Photos",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ApplicationUserId",
                table: "Photos",
                newName: "IX_Photos_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LifeEvents",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Languages",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Interests",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Educations",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "LifeEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "LifeEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Languages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Interests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Interests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Educations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8be746f1-bec1-4a0d-a327-7c067e1e7ab2", "8d225d5e-569b-4065-9b11-bb36173bc807", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a75b819-ad86-48c1-9dca-bed01d2c8e29", "8bcbaacd-54db-4f5d-b7ea-07415fc8690e", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ApplicationId",
                table: "Videos",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApplicationId",
                table: "Skills",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_ApplicationId",
                table: "LifeEvents",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ApplicationId",
                table: "Languages",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_ApplicationId",
                table: "Interests",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ApplicationId",
                table: "Educations",
                column: "ApplicationId");

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
                name: "FK_Photos_AspNetUsers_ApplicationId",
                table: "Photos",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Photos_AspNetUsers_ApplicationId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_ApplicationId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_ApplicationId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ApplicationId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ApplicationId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_LifeEvents_ApplicationId",
                table: "LifeEvents");

            migrationBuilder.DropIndex(
                name: "IX_Languages_ApplicationId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Interests_ApplicationId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Educations_ApplicationId",
                table: "Educations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a75b819-ad86-48c1-9dca-bed01d2c8e29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be746f1-bec1-4a0d-a327-7c067e1e7ab2");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "LifeEvents");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Videos",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Skills",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Photos",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_ApplicationId",
                table: "Photos",
                newName: "IX_Photos_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "LifeEvents",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Languages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Interests",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Educations",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LifeEvents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Languages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Interests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Educations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70afc07a-89a0-4860-8a51-f6244b4dde09", "fc2f35e8-c7b8-4bc4-a589-124527d9c362", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71352d4b-6646-4fcc-9d3e-524071b8fa91", "c3159481-a8fb-4416-a9bc-1fd1f9fdd396", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UserId",
                table: "Videos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserId",
                table: "Skills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_UserId",
                table: "LifeEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UserId",
                table: "Languages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_UserId",
                table: "Interests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_AspNetUsers_UserId",
                table: "Interests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_AspNetUsers_UserId",
                table: "Languages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeEvents_AspNetUsers_UserId",
                table: "LifeEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_ApplicationUserId",
                table: "Photos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_UserId",
                table: "Skills",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
