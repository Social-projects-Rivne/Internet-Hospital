using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Updated_database_for_feedbacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_feedBackTypes_TypeId",
                table: "FeedBacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_feedBackTypes",
                table: "feedBackTypes");

            migrationBuilder.RenameTable(
                name: "feedBackTypes",
                newName: "FeedBackTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedBackTypes",
                table: "FeedBackTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_FeedBackTypes_TypeId",
                table: "FeedBacks",
                column: "TypeId",
                principalTable: "FeedBackTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_FeedBackTypes_TypeId",
                table: "FeedBacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedBackTypes",
                table: "FeedBackTypes");

            migrationBuilder.RenameTable(
                name: "FeedBackTypes",
                newName: "feedBackTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_feedBackTypes",
                table: "feedBackTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_feedBackTypes_TypeId",
                table: "FeedBacks",
                column: "TypeId",
                principalTable: "feedBackTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
