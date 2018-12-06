using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class addedarticlestatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleStatusId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ArticleStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Active" });

            migrationBuilder.InsertData(
                table: "ArticleStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Deleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleStatusId",
                table: "Articles",
                column: "ArticleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleStatuses_ArticleStatusId",
                table: "Articles",
                column: "ArticleStatusId",
                principalTable: "ArticleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleStatuses_ArticleStatusId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleStatusId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleStatusId",
                table: "Articles");
        }
    }
}
