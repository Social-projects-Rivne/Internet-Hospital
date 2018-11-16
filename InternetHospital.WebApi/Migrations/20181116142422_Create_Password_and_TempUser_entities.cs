using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Create_Password_and_TempUser_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportURL",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedTime",
                table: "Diplomas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Passport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    PassportURL = table.Column<string>(nullable: true),
                    IsValid = table.Column<bool>(nullable: true),
                    AddedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passport_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    ThirdName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LicenseURL = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    AddedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Banned user who has violated our rules.");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "New user registered in our system.");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Approved user with checked data.");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Not approved, because user`s data was invalid.");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Deleted user by Admin or Moderator.");

            migrationBuilder.CreateIndex(
                name: "IX_Passport_UserId",
                table: "Passport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUser_UserId",
                table: "TemporaryUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passport");

            migrationBuilder.DropTable(
                name: "TemporaryUser");

            migrationBuilder.DropColumn(
                name: "AddedTime",
                table: "Diplomas");

            migrationBuilder.AddColumn<string>(
                name: "PassportURL",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Banned user who has violated our rules");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "New user registered in our system");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Approved user with checked data");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Not approved, because user`s data was invalid");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Deleted user by Admin or Moderator");
        }
    }
}
