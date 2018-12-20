using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Addtableuserrequesttypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRequestTypeId",
                table: "TemporaryUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserRequests",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 1, "Request to edit profile" });

            migrationBuilder.InsertData(
                table: "UserRequests",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 2, "Request to become a doctor" });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUsers_UserRequestTypeId",
                table: "TemporaryUsers",
                column: "UserRequestTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryUsers_UserRequests_UserRequestTypeId",
                table: "TemporaryUsers",
                column: "UserRequestTypeId",
                principalTable: "UserRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryUsers_UserRequests_UserRequestTypeId",
                table: "TemporaryUsers");

            migrationBuilder.DropTable(
                name: "UserRequests");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryUsers_UserRequestTypeId",
                table: "TemporaryUsers");

            migrationBuilder.DropColumn(
                name: "UserRequestTypeId",
                table: "TemporaryUsers");
        }
    }
}
