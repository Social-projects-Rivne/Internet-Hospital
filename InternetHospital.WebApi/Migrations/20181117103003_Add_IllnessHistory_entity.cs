using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Add_IllnessHistory_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passport_AspNetUsers_UserId",
                table: "Passport");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryUser_AspNetUsers_UserId",
                table: "TemporaryUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemporaryUser",
                table: "TemporaryUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passport",
                table: "Passport");

            migrationBuilder.RenameTable(
                name: "TemporaryUser",
                newName: "TemporaryUsers");

            migrationBuilder.RenameTable(
                name: "Passport",
                newName: "Passports");

            migrationBuilder.RenameIndex(
                name: "IX_TemporaryUser_UserId",
                table: "TemporaryUsers",
                newName: "IX_TemporaryUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Passport_UserId",
                table: "Passports",
                newName: "IX_Passports_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemporaryUsers",
                table: "TemporaryUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passports",
                table: "Passports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IllnessHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false),
                    Symptoms = table.Column<string>(nullable: true),
                    Diagnose = table.Column<string>(nullable: true),
                    Treatment = table.Column<string>(nullable: true),
                    ConclusionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IllnessHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IllnessHistories_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IllnessHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IllnessHistories_DoctorId",
                table: "IllnessHistories",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_IllnessHistories_UserId",
                table: "IllnessHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_AspNetUsers_UserId",
                table: "Passports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryUsers_AspNetUsers_UserId",
                table: "TemporaryUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_AspNetUsers_UserId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryUsers_AspNetUsers_UserId",
                table: "TemporaryUsers");

            migrationBuilder.DropTable(
                name: "IllnessHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemporaryUsers",
                table: "TemporaryUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passports",
                table: "Passports");

            migrationBuilder.RenameTable(
                name: "TemporaryUsers",
                newName: "TemporaryUser");

            migrationBuilder.RenameTable(
                name: "Passports",
                newName: "Passport");

            migrationBuilder.RenameIndex(
                name: "IX_TemporaryUsers_UserId",
                table: "TemporaryUser",
                newName: "IX_TemporaryUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Passports_UserId",
                table: "Passport",
                newName: "IX_Passport_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemporaryUser",
                table: "TemporaryUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passport",
                table: "Passport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passport_AspNetUsers_UserId",
                table: "Passport",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryUser_AspNetUsers_UserId",
                table: "TemporaryUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
