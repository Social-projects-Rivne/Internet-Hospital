using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Addbooleanpropertytoappointmententity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowPatientInfo",
                table: "Appointments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowPatientInfo",
                table: "Appointments");
        }
    }
}
