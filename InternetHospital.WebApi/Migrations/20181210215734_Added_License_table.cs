using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Added_License_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_Doctors_DoctorId",
                table: "Diplomas");

            migrationBuilder.DropColumn(
                name: "LicenseURL",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "LicenseURL",
                table: "TemporaryUsers",
                newName: "Specialization");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Diplomas",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Diplomas_DoctorId",
                table: "Diplomas",
                newName: "IX_Diplomas_UserId");

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    LicenseURL = table.Column<string>(nullable: true),
                    IsValid = table.Column<bool>(nullable: true),
                    AddedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_UserId",
                table: "Licenses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_AspNetUsers_UserId",
                table: "Diplomas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_AspNetUsers_UserId",
                table: "Diplomas");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "TemporaryUsers",
                newName: "LicenseURL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Diplomas",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Diplomas_UserId",
                table: "Diplomas",
                newName: "IX_Diplomas_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "LicenseURL",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_Doctors_DoctorId",
                table: "Diplomas",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
