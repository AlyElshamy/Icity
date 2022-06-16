using Microsoft.EntityFrameworkCore.Migrations;

namespace Icity.Migrations
{
    public partial class ProfileModelUpIden2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_AspNetUsers_UserId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_AspNetUsers_UserId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_AspNetUsers_UserId",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeEvent_AspNetUsers_UserId",
                table: "LifeEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_AspNetUsers_UserId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Video",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_UserId",
                table: "Photo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeEvent",
                table: "LifeEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interest",
                table: "Interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64d8822d-46a9-4bdc-a1cc-67c5b0a35828");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef775727-0bd8-4374-8a38-e24ac6443894");

            migrationBuilder.RenameTable(
                name: "Video",
                newName: "Videos");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameTable(
                name: "LifeEvent",
                newName: "LifeEvents");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Interest",
                newName: "Interests");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameIndex(
                name: "IX_Video_UserId",
                table: "Videos",
                newName: "IX_Videos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skill_UserId",
                table: "Skills",
                newName: "IX_Skills_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Photos",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_LifeEvent_UserId",
                table: "LifeEvents",
                newName: "IX_LifeEvents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_UserId",
                table: "Languages",
                newName: "IX_Languages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_UserId",
                table: "Interests",
                newName: "IX_Interests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_UserId",
                table: "Educations",
                newName: "IX_Educations_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "PhotoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeEvents",
                table: "LifeEvents",
                column: "LifeEventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "LanguageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interests",
                table: "Interests",
                column: "InterestID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "EducationID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70afc07a-89a0-4860-8a51-f6244b4dde09", "fc2f35e8-c7b8-4bc4-a589-124527d9c362", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71352d4b-6646-4fcc-9d3e-524071b8fa91", "c3159481-a8fb-4416-a9bc-1fd1f9fdd396", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ApplicationUserId",
                table: "Photos",
                column: "ApplicationUserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ApplicationUserId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeEvents",
                table: "LifeEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70afc07a-89a0-4860-8a51-f6244b4dde09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71352d4b-6646-4fcc-9d3e-524071b8fa91");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "Video");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameTable(
                name: "LifeEvents",
                newName: "LifeEvent");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "Interest");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UserId",
                table: "Video",
                newName: "IX_Video_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_UserId",
                table: "Skill",
                newName: "IX_Skill_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Photo",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LifeEvents_UserId",
                table: "LifeEvent",
                newName: "IX_LifeEvent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_UserId",
                table: "Language",
                newName: "IX_Language_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_UserId",
                table: "Interest",
                newName: "IX_Interest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserId",
                table: "Education",
                newName: "IX_Education_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Photo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Video",
                table: "Video",
                column: "VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "SkillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "PhotoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeEvent",
                table: "LifeEvent",
                column: "LifeEventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "LanguageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interest",
                table: "Interest",
                column: "InterestID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "EducationID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64d8822d-46a9-4bdc-a1cc-67c5b0a35828", "febb7c3d-5393-4a4a-8687-ad78bfa1f330", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef775727-0bd8-4374-8a38-e24ac6443894", "87db9f46-9cfe-45e5-8760-b67fce8c1778", "Customer", "CUSTOMER" });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_AspNetUsers_UserId",
                table: "Education",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_AspNetUsers_UserId",
                table: "Interest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Language_AspNetUsers_UserId",
                table: "Language",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeEvent_AspNetUsers_UserId",
                table: "LifeEvent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_AspNetUsers_UserId",
                table: "Skill",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
